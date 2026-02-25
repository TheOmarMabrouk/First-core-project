using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using First_core_project.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

    // GET: /api/OrdersAPI/my-orders
    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var orders = await _context.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatAt)
            .ToListAsync();

        var ordersDto = new List<object>();

        foreach (var order in orders)
        {
            var items = await _context.OrderItems
                .Where(oi => oi.OrderId == order.Id)
                .Select(oi => new
                {
                    oi.ProductId,
                    oi.ProductName,
                    oi.Price,
                    Quantity = oi.Quantity,
                    Total = oi.Total
                })
                .ToListAsync();

            ordersDto.Add(new
            {
                order.Id,
                order.TotalPrice,
                order.Status,
                order.CreatAt,
                order.PaidAt,
                Items = items
            });
        }

        return Ok(new
        {
            success = true,
            count = ordersDto.Count,
            data = ordersDto
        });
    }

    // POST: /api/OrdersAPI/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.Carts
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        if (!cartItems.Any())
            return BadRequest(new { success = false, message = "السلة فارغة" });

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

        return Ok(new { success = true, message = "تم إنشاء الطلب بنجاح", orderId = order.Id });
    }
}