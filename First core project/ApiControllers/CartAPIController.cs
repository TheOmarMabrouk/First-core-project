using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using First_core_project.Services.API;
using First_core_project.Helpers;

namespace First_core_project.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private readonly IApiCartService _cartService;
        public CartAPIController(IApiCartService cartService) => _cartService = cartService;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _cartService.GetUserCartAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), $"{Request.Scheme}://{Request.Host}/Images/"));

        [HttpPost("add/{productId}")]
        public async Task<IActionResult> Add(int productId)
        {
            await _cartService.AddToCartAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), productId);
            return Ok(new ApiResponse<object>(true, "تمت الإضافة للسلة", null));
        }
    }
}