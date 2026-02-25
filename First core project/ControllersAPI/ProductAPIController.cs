using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using First_core_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

[Route("api/[controller]")]
[ApiController]
public class ProductsAPIController : ControllerBase
{
    private readonly SouqcomContext _context;

    public ProductsAPIController(SouqcomContext context)
    {
        _context = context;
    }

    // GET: /api/ProductsAPI
    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? categoryId = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null // "price_asc", "price_desc", "entrydate_desc"
    )
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}/Images/";

        var query = _context.Products.AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(p => p.Catid == categoryId);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name!.Contains(search));

        // Sorting
        query = sort switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "entrydate_desc" => query.OrderByDescending(p => p.EntryDate),
            _ => query.OrderBy(p => p.Id)
        };

        var totalCount = await query.CountAsync();
        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Discription,
                p.Price,
                CategoryId = p.Catid,
                p.Type,
                p.SupplierName,
                EntryDate = p.EntryDate.HasValue ? p.EntryDate.Value.ToString("yyyy-MM-dd") : null,
                Images = p.ProductImages.Select(img => baseUrl + (img.Image ?? "default.jpg")).ToList(),
                ReviewUrl = p.ReviewUrl,
                MainImage = baseUrl + (p.Photo ?? "default.jpg")
            })
            .ToListAsync();

        return Ok(new
        {
            success = true,
            page,
            pageSize,
            totalCount,
            count = products.Count,
            data = products
        });
    }

    // POST: api/ProductsAPI
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Super Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "تمت إضافة المنتج بنجاح", productId = product.Id });
    }
}