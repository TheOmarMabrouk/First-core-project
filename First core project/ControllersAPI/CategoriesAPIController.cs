using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using First_core_project.Models;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class CategoriesAPIController : ControllerBase
{
    private readonly SouqcomContext _context;

    public CategoriesAPIController(SouqcomContext context)
    {
        _context = context;
    }

    // GET: api/CategoriesAPI
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _context.Categories
            .Select(c => new
            {
                c.Id,
                c.Name
            })
            .ToListAsync();

        return Ok(new
        {
            success = true,
            count = categories.Count,
            data = categories
        });
    }

    // POST: api/CategoriesAPI
    [Authorize(Roles = "Super Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] string name)
    {
        var category = new Category
        {
            Name = name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            message = "تمت إضافة القسم بنجاح"
        });
    }
}