using First_core_project.DTOs.API;

namespace First_core_project.Services.API
{
    public interface IApiCartService
    {
        Task<ApiCartDto> GetUserCartAsync(string userId, string baseUrl);
        Task AddToCartAsync(string userId, int productId);
    }
}