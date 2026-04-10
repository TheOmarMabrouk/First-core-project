using First_core_project.DTOs.API;
using First_core_project.Models;

namespace First_core_project.Services.API
{
    public interface IApiProductService
    {
        Task<(List<ApiProductDto> products, int totalCount)> GetAllProductsAsync(int page, int pageSize, int? categoryId, string? search, string? sort, string baseUrl);
        Task<int> CreateProductAsync(Product product);
    }
}











