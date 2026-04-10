using First_core_project.Models;
using First_core_project.DTOs.API;
using Microsoft.EntityFrameworkCore;

namespace First_core_project.Services.API
{
    public class ApiCartService : IApiCartService
    {
        private readonly SouqcomContext _context;
        public ApiCartService(SouqcomContext context) => _context = context;

        public async Task<ApiCartDto> GetUserCartAsync(string userId, string baseUrl)
        {
            // تأكد من أسماء الأعمدة هنا p.Productid و c.Qty حسب ملف الـ Model عندك
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .Select(c => new ApiCartItemDto
                {
                    CartItemId = c.Id,
                    ProductId = (int)c.ProductId, // جرب تخليها Productid (i صغيرة) لو مطلع ايرور
                    ProductName = c.Product.Name,
                    Price = c.Product.Price,
                    Quantity = (int)c.Qty, // جرب تخليها Qty لو Quantity غلط
                    MainImage = baseUrl + (c.Product.Photo ?? "default.jpg")
                }).ToListAsync();

            return new ApiCartDto
            {
                Items = cartItems,
                TotalItems = cartItems.Count,
                TotalPrice = cartItems.Sum(i => (i.Price ?? 0) * i.Quantity)
            };
        }

        public async Task AddToCartAsync(string userId, int productId)
        {
            // بنشيك لو المنتج موجود أصلاً في السلة لليوزر ده
            var item = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (item != null)
            {
                item.Qty++;
            }
            else
            {
                _context.Carts.Add(new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Qty = 1
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}