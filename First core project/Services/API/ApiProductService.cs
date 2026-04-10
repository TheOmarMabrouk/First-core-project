
using First_core_project.DTOs.API;
using First_core_project.Models;
using First_core_project.Services.API;
using Microsoft.EntityFrameworkCore;


namespace First_core_project.Services.API

{
    public class ApiProductService : IApiProductService
    {
        private readonly SouqcomContext _context;

        public ApiProductService(SouqcomContext context)
        {
            _context = context;
        }

        public async Task<(List<ApiProductDto> products, int totalCount)> GetAllProductsAsync(int page, int pageSize, int? categoryId, string? search, string? sort, string baseUrl)
        {
            var query = _context.Products.AsQueryable();

            if (categoryId.HasValue) query = query.Where(p => p.Catid == categoryId);
            if (!string.IsNullOrEmpty(search)) query = query.Where(p => p.Name!.Contains(search));

            query = sort switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Id)
            };

            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ApiProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Discription,
                    Price = p.Price,
                    MainImage = baseUrl + (p.Photo ?? "default.jpg"),
                    Images = p.ProductImages.Select(img => baseUrl + (img.Image ?? "default.jpg")).ToList()
                }).ToListAsync();

            return (products, totalCount);
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
    }
}
