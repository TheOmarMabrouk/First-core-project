using First_core_project.DTOs;
using First_core_project.Models;
using Microsoft.EntityFrameworkCore;

namespace First_core_project.Services
{
    public class OrderService : IOrderService
    {
        private readonly SouqcomContext _db;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(SouqcomContext db,
            IPaymentService paymentService,
            ILogger<OrderService> logger)
        {
            _db = db;
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<CheckoutResultDto> CheckoutAsync(string userId)
        {
            _logger.LogInformation("Checkout started for user {UserId}", userId);

            var cartItems = await _db.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                _logger.LogWarning("Cart is empty for user {UserId}", userId);

                return new CheckoutResultDto
                {
                    Success = false,
                    Message = "Cart is empty"
                };
            }

            var products = await _db.Products.ToDictionaryAsync(p => p.Id);

            decimal total = 0;

            foreach (var item in cartItems)
            {
                if (products.TryGetValue((int)item.ProductId, out var product))
                {
                    total += (decimal)((product.Price ?? 0) * (item.Qty ?? 0));
                }
                else
                {
                    _logger.LogWarning(
                        "Product not found for ProductId {ProductId}",
                        item.ProductId);
                }
            }

            var order = new Order
            {
                UserId = userId,
                TotalPrice = total,
                Status = 0,
                CreatAt = DateTime.UtcNow
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            foreach (var item in cartItems)
            {
                if (products.TryGetValue((int)item.ProductId, out var product))
                {
                    _db.OrderItems.Add(new OrderItem
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

            _db.Carts.RemoveRange(cartItems);
            await _db.SaveChangesAsync();

            try
            {
                _logger.LogInformation(
                    "Creating payment for OrderId {OrderId} with total {Total}",
                    order.Id, total);

                var paymentUrl =
                    await _paymentService.CreatePaymentAsync(order.Id, total);

                _logger.LogInformation(
                    "Payment URL generated successfully for OrderId {OrderId}",
                    order.Id);

                return new CheckoutResultDto
                {
                    Success = true,
                    PaymentUrl = paymentUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Payment failed for OrderId {OrderId}",
                    order.Id);

                throw; // يخلي الـ Global Middleware يمسكها
            }
        }

        public async Task MarkOrderAsPaidAsync(int orderId)
        {
            _logger.LogInformation(
                "MarkOrderAsPaid called for OrderId {OrderId}",
                orderId);

            var order = await _db.Orders.FindAsync(orderId);

            if (order == null)
            {
                _logger.LogWarning(
                    "Order not found for OrderId {OrderId}",
                    orderId);
                return;
            }

            if (order.Status == 0)
            {
                order.Status = 1;
                await _db.SaveChangesAsync();

                _logger.LogInformation(
                    "Order marked as paid successfully for OrderId {OrderId}",
                    orderId);
            }
            else
            {
                _logger.LogWarning(
                    "Order {OrderId} is already paid",
                    orderId);
            }
        }
    }
}