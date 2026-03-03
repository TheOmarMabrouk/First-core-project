using First_core_project.Data;
using First_core_project.Dtos;
using First_core_project.Models;
using Microsoft.EntityFrameworkCore;

namespace First_core_project.Services
{
    public class CartService : ICartService
    {
        private readonly SouqcomContext _db;

        public CartService(SouqcomContext db)
        {
            _db = db;
        }

        public async Task AddToCartAsync(int productId, string userId)
        {
            var existingItem = await _db.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Qty += 1;
            }
            else
            {
                await _db.Carts.AddAsync(new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Qty = 1
                });
            }

            await _db.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetCartItemsAsync(string userId)
        {
            return await _db.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public Task<decimal> GetCartTotalAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFromCartAsync(int cartId, string userId)
        {
            var item = await _db.Carts
                .FirstOrDefaultAsync(c => c.Id == cartId && c.UserId == userId);

            if (item != null)
            {
                _db.Carts.Remove(item);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<CartUpdateDto> UpdateQuantityAsync(int cartId, string userId, string operation)
        {
            var item = await _db.Carts
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == cartId && x.UserId == userId);

            if (item == null)
                return new CartUpdateDto { Success = false };

            if (operation == "increase")
                item.Qty += 1;
            else if (operation == "decrease" && item.Qty > 1)
                item.Qty -= 1;

            await _db.SaveChangesAsync();

            var itemTotal = item.Qty * item.Product.Price;

            var cartTotal = await _db.Carts
                .Include(x => x.Product)
                .Where(c => c.UserId == userId)
                .SumAsync(x => x.Qty * x.Product.Price);

            return new CartUpdateDto
            {
                Success = true,
                NewQty = item.Qty ?? 0,
                ItemTotal = (item.Qty ?? 0) * (item.Product?.Price ?? 0),
                CartTotal = (decimal)cartTotal
            };
        }
    }
}