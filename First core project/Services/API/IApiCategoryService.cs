using First_core_project.DTOs.API;

namespace First_core_project.Services.API
{
    public interface IApiCategoryService
    {
        Task<List<ApiCategoryDto>> GetAllCategoriesAsync();
        Task<int> CreateCategoryAsync(string name);
    }
}
