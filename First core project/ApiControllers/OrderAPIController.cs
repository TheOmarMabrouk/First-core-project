using First_core_project.Helpers;
using First_core_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace First_core_project.Controllers.API
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersAPIController : ControllerBase
    {
        private readonly SouqcomContext _context;

        public OrdersAPIController(SouqcomContext context)
        {
            _context = context;
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // 1. جلب الطلبات الأساسية
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatAt)
                .ToListAsync();

            var ordersDto = new List<object>();

            // 2. جلب تفاصيل كل طلب يدوياً لضمان عدم حدوث إيرور في الـ Select
            foreach (var order in orders)
            {
                var items = await _context.OrderItems
                    .Where(oi => oi.OrderId == order.Id)
                    .Select(oi => new
                    {
                        oi.ProductId,
                        oi.ProductName,
                        oi.Price,
                        oi.Quantity,
                        oi.Total
                    })
                    .ToListAsync();

                ordersDto.Add(new
                {
                    order.Id,
                    order.TotalPrice,
                    order.Status,
                    CreatedAt = order.CreatAt.HasValue ? order.CreatAt.Value.ToString("yyyy-MM-dd HH:mm") : null,
                    Items = items
                });
            }

            // 3. الرد الموحد "عظمة العظمة"
            return Ok(new ApiResponse<object>(true, "تم جلب طلباتك بنجاح", ordersDto));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
                return BadRequest(new ApiResponse<object>(false, "السلة فارغة، لا يمكن إنشاء طلب"));

            var order = new Order
            {
                UserId = userId,
                TotalPrice = cartItems.Sum(c => c.Qty * (c.Product.Price ?? 0)),
                Status = 0,
                CreatAt = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderItems = cartItems.Select(c => new OrderItem
            {
                OrderId = order.Id,
                ProductId = c.ProductId,
                ProductName = c.Product.Name,
                Price = c.Product.Price,
                Quantity = c.Qty,
                Total = c.Qty * (c.Product.Price ?? 0)
            }).ToList();

            _context.OrderItems.AddRange(orderItems);
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<object>(true, "تم إنشاء الطلب بنجاح", new { orderId = order.Id }));
        }
    }
}