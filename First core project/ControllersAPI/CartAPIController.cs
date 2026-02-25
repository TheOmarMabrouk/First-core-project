using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using First_core_project.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CartAPIController : ControllerBase
{
    private readonly SouqcomContext _context;

    public CartAPIController(SouqcomContext context)
    {
        _context = context;
    }

    // GET: api/CartAPI
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.Carts
            .Include(c => c.Product)
                .ThenInclude(p => p.ProductImages)
            .Where(c => c.UserId == userId)
            .Select(c => new
            {
                c.Id,
                c.ProductId,
                c.Product.Name,
                c.Product.Price,
                c.Qty,
                Images = c.Product.ProductImages.Select(img => "/Images/" + (img.Image ?? "default.jpg")).ToList(),
                MainImage = "/Images/" + (c.Product.Photo ?? "default.jpg")
            })
            .ToListAsync();

        return Ok(new
        {
            success = true,
            count = cartItems.Count,
            data = cartItems
        });
    }

    // POST: api/CartAPI/add
    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItem = await _context.Carts
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        if (cartItem != null)
        {
            cartItem.Qty += 1;
        }
        else
        {
            cartItem = new Cart
            {
                UserId = userId,
                ProductId = productId,
                Qty = 1
            };
            _context.Carts.Add(cartItem);
        }

        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "تمت إضافة المنتج للسلة" });
    }

    // DELETE: api/CartAPI/remove/5
    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItem = await _context.Carts
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (cartItem == null)
            return NotFound(new { success = false, message = "العنصر غير موجود في السلة" });

        _context.Carts.Remove(cartItem);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "تم حذف المنتج من السلة" });
    }
}