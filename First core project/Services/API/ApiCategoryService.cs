using First_core_project.DTOs.API;
using First_core_project.Models;
using Microsoft.EntityFrameworkCore;

namespace First_core_project.Services.API
{
    public class ApiCategoryService : IApiCategoryService
    {
        private readonly SouqcomContext _context;
        public ApiCategoryService(SouqcomContext context) => _context = context;
        public async Task<List<ApiCategoryDto>> GetAllCategoriesAsync() =>
            await _context.Categories.Select(c => new ApiCategoryDto { Id = c.Id, Name = c.Name }).ToListAsync();
        public async Task<int> CreateCategoryAsync(string name)
        {
            var cat = new Category { Name = name };
            _context.Categories.Add(cat);
            await _context.SaveChangesAsync();
            return cat.Id;
        }
    }
}
