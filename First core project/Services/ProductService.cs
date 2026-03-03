using First_core_project.Helpers;
using First_core_project.Models;
using Microsoft.EntityFrameworkCore;

namespace First_core_project.Services
{
    public class ProductService : IProductService
    {
        private readonly SouqcomContext _db;

        public ProductService(SouqcomContext db)
        {
            _db = db;
        }

        // 🟢 Get All Categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        // 🟢 Get Products By Category + Pagination + Sorting
        public async Task<List<Product>> GetProductsByCategoryAsync(
            int categoryId,
            PaginationParams param)
        {
            var query = _db.Products
                .Where(x => x.Catid == categoryId)
                .AsQueryable();

            return await query
                .ApplyPagination(param)
                .ToListAsync();
        }

        // 🟢 Product Details
        public async Task<Product?> GetProductDetailsAsync(int id)
        {
            return await _db.Products
                .Include(x => x.Cat)
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // 🟢 Search + Pagination + Sorting
        public async Task<List<Product>> SearchProductsAsync(
            string? name,
            PaginationParams param)
        {
            var query = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            return await query
                .ApplyPagination(param)
                .ToListAsync();
        }

        public string? SearchProducts(string xname)
        {
            throw new NotImplementedException();
        }

        public string? GetProductDetails(int id)
        {
            throw new NotImplementedException();
        }

        public string? GetProductsByCategory(int id, string sortOrder)
        {
            throw new NotImplementedException();
        }

        public string? GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}