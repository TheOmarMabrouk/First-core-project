using First_core_project.Data;
using First_core_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

[Authorize]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _identityDb;
    private readonly SouqcomContext db;

    
    private readonly string _paymobApiKey = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2ljSEp2Wm1sc1pWOXdheUk2TVRFek1qVTVNaXdpYm1GdFpTSTZJakUzTnpFMk1qYzJNak11TkRjek5qTWlmUS5VVE1fWVc0R3BIQURIZERQLWEydVZuRllSQUluaFlpVWtLSjdnRjAtMWNhTVBEbHI5U092SnEzUmJOc214MTBNMDhmcjh2WXBNQU1ORE5wYk5KU290Zw==";
    private readonly string _integrationId = "5548480";
    private readonly string _iframeId = "1008152";

    public OrdersController(ApplicationDbContext identityDb, SouqcomContext DB)
    {
        _identityDb = identityDb;
        db = DB;
    }

    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // 1. سحب بيانات السلة
        var cartItems = db.Carts.Where(c => c.UserId == userId).ToList();
        if (!cartItems.Any()) return BadRequest("سلة المشتريات فارغة");

        var products = db.Products.ToDictionary(p => p.Id, p => p);

        // 2. حساب الإجمالي
        decimal total = 0;
        foreach (var item in cartItems)
        {
            if (products.TryGetValue((int)item.ProductId, out var product))
                total += (decimal)((product.Price ?? 0) * (item.Qty ?? 0));
        }

        // 3. إنشاء الأوردر في قاعدة بياناتك (Database First)
        var order = new Order
        {
            UserId = userId,
            TotalPrice = total,
            Status = 0, // Pending
            CreatAt = DateTime.Now
        };

        db.Orders.Add(order);
        await db.SaveChangesAsync();

        // 4. حفظ المنتجات في الطلب
        foreach (var item in cartItems)
        {
            if (products.TryGetValue((int)item.ProductId, out var product))
            {
                db.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = item.Qty,
                    Total = (product.Price ?? 0) * (item.Qty ?? 0)
                });
            }
        }
        await db.SaveChangesAsync();

        // 5. مسح السلة بعد تسجيل الطلب
        db.Carts.RemoveRange(cartItems);
        await db.SaveChangesAsync();

        // 6. رحلة Paymob
        try
        {
            // أ- الحصول على التوكن
            string authToken = await GetPaymobToken();

            // ب- تسجيل الطلب في بي موب (استخدام رابط ecommerce/orders لتجنب 404)
            int paymobOrderId = await RegisterPaymobOrder(authToken, total, order.Id);

            // ج- جلب مفتاح الدفع
            string paymentKey = await GetPaymentKey(authToken, paymobOrderId, total);

            // د- التوجيه لصفحة الدفع
            string iframeUrl = $"https://accept.paymob.com/api/acceptance/iframes/{_iframeId}?payment_token={paymentKey}";

            return Redirect(iframeUrl);
        }
        catch (Exception ex)
        {
            // في حال حدوث خطأ اطبع التفاصيل
            return Content($"🔥 Paymob Error: {ex.Message}");
        }
    }

    private async Task<string> GetPaymobToken()
    {
        using var client = new HttpClient();
        var data = new { api_key = _paymobApiKey };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://accept.paymob.com/api/auth/tokens", content);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Token Error: {json}");

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("token").GetString();
    }

    private async Task<int> RegisterPaymobOrder(string token, decimal amount, int merchantOrderId)
    {
        using var client = new HttpClient();
        var data = new
        {
            auth_token = token,
            delivery_needed = false,
            amount_cents = (long)(amount * 100),
            currency = "EGP",
            merchant_order_id = merchantOrderId.ToString(), // ربط مع ID قاعدة بياناتك
            items = Array.Empty<object>()
        };

        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        // الرابط المحدث لتجنب Not Found
        var response = await client.PostAsync("https://accept.paymob.com/api/ecommerce/orders", content);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Order Registration Error: {json}");

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("id").GetInt32();
    }

    private async Task<string> GetPaymentKey(string token, int paymobOrderId, decimal amount)
    {
        using var client = new HttpClient();
        var data = new
        {
            auth_token = token,
            amount_cents = (long)(amount * 100),
            expiration = 3600,
            order_id = paymobOrderId.ToString(),
            billing_data = new
            {
                apartment = "NA",
                email = "test@user.com",
                floor = "NA",
                first_name = "Customer",
                street = "NA",
                building = "NA",
                phone_number = "01000000000",
                shipping_method = "NA",
                postal_code = "NA",
                city = "Cairo",
                country = "EG",
                last_name = "Name",
                state = "NA"
            },
            currency = "EGP",
            integration_id = int.Parse(_integrationId)
        };

        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://accept.paymob.com/api/acceptance/payment_keys", content);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"PaymentKey Error: {json}");

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("token").GetString();
    }

    [HttpGet]
    public async Task<IActionResult> PaymentCallback([FromQuery] string success, [FromQuery] string merchant_order_id)
    {
        // التحقق من نجاح العملية وتحديث قاعدة البيانات
        if (success == "true" && int.TryParse(merchant_order_id, out int orderId))
        {
            var dbOrder = db.Orders.FirstOrDefault(o => o.Id == orderId);
            if (dbOrder != null && dbOrder.Status == 0)
            {
                dbOrder.Status = 1; // تحويل لـ Paid
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Success", new { id = orderId });
        }

        return Content("فشلت عملية الدفع أو تم إلغاؤها.");
    }

    public IActionResult Success(int id)
    {
        ViewBag.OrderId = id;
        return View();
    }
}