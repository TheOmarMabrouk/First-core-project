using First_core_project.Helpers;
using First_core_project.Models;

public interface IProductService
{
    Task<List<Category>> GetCategoriesAsync();

    Task<List<Product>> GetProductsByCategoryAsync(
        int categoryId,
        PaginationParams param);

    Task<Product?> GetProductDetailsAsync(int id);

    Task<List<Product>> SearchProductsAsync(
        string? name,
        PaginationParams param);
   
}