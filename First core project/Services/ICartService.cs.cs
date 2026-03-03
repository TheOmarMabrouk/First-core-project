using First_core_project.Dtos;
using First_core_project.Models;

namespace First_core_project.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(int productId, string userId);
        Task<List<Cart>> GetCartItemsAsync(string userId);
        Task RemoveFromCartAsync(int cartId, string userId);
        Task<CartUpdateDto> UpdateQuantityAsync(int cartId, string userId, string operation);
        Task<decimal> GetCartTotalAsync(string userId);
    }
}