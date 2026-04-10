using First_core_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // ================================
    // Checkout
    // ================================
    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _orderService.CheckoutAsync(userId);

        if (!result.Success)
            return BadRequest(result.Message);

        return Redirect(result.PaymentUrl);
    }

    // ================================
    // Payment Callback
    // ================================
    [HttpGet]
    public async Task<IActionResult> PaymentCallback(string success, string merchant_order_id)
    {
        if (success == "true" && int.TryParse(merchant_order_id, out int orderId))
        {
            await _orderService.MarkOrderAsPaidAsync(orderId);
            return RedirectToAction("Success", new { id = orderId });
        }

        return Content("فشلت عملية الدفع أو تم إلغاؤها.");
    }

    // ================================
    // Success Page
    // ================================
    [HttpGet]
    public IActionResult Success(int id)
    {
        ViewBag.OrderId = id;
        return View();
    }


}