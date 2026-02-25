using First_core_project.Data;
using First_core_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_core_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class OrdersController : Controller
    {
        private readonly SouqcomContext _db;
        private readonly ApplicationDbContext _identityDb;

        public OrdersController(SouqcomContext db, ApplicationDbContext identityDb)
        {
            _db = db;
            _identityDb = identityDb;
        }

        // 1. عرض قائمة بكل الطلبات (باستثناء الملغية لتسريع العمل)
        public IActionResult Index()
        {
            // جلب الطلبات غير الملغية (Status != 4) من الأحدث للأقدم
            var allOrders = _db.Orders
                .Where(o => o.Status != 4)
                .OrderByDescending(o => o.CreatAt)
                .ToList();

            // ربط معرفات المستخدمين بإيميلاتهم من قاعدة بيانات الـ Identity
            var users = _identityDb.Users.ToDictionary(u => u.Id, u => u.Email);
            ViewBag.Users = users;

            return View(allOrders);
        }

        // 2. عرض تفاصيل طلب معين (الفاتورة وتغيير الحالة)
        public IActionResult Details(int id)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            // جلب أصناف الطلب (المنتجات اللي جوه الأوردر ده)
            var items = _db.OrderItems.Where(i => i.OrderId == id).ToList();
            ViewBag.Items = items;

            // جلب إيميل العميل لعرضه في تفاصيل الفاتورة
            var user = _identityDb.Users.FirstOrDefault(u => u.Id == order.UserId);
            ViewBag.CustomerEmail = user?.Email;

            return View(order);
        }

        // 3. أكشن لتحديث حالة الطلب (من قائمة منسدلة)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int orderId, int newStatus)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = $"تم تحديث حالة الطلب #{orderId} بنجاح.";
            }
            return RedirectToAction(nameof(Index));
        }

        // 4. أكشن إلغاء الطلب (إخفاء من القائمة الرئيسية وتحويله للأرشيف)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelOrder(int orderId, string reason)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order == null) return NotFound();

            // تحويل الحالة لـ 4 (Cancelled)
            order.Status = 4;

            // ملاحظة: لو عندك عمود للسبب في الداتا بيز ممكن تضيفه هنا
            // order.CancelReason = reason;

            await _db.SaveChangesAsync();

            // رسالة تنبيه تظهر للأدمن بعد الإلغاء
            TempData["SuccessMessage"] = $"تم إلغاء الطلب رقم {orderId} بنجاح واختفى من القائمة الحالية.";

            return RedirectToAction(nameof(Index));
        }

        // 5. اختياري: عرض الطلبات الملغية فقط (الأرشيف)
        public IActionResult CancelledOrders()
        {
            var cancelledOrders = _db.Orders
                .Where(o => o.Status == 4)
                .OrderByDescending(o => o.CreatAt)
                .ToList();

            var users = _identityDb.Users.ToDictionary(u => u.Id, u => u.Email);
            ViewBag.Users = users;

            return View("Index", cancelledOrders); // إعادة استخدام نفس الفيو (Index) لعرض البيانات
        }
    }
}