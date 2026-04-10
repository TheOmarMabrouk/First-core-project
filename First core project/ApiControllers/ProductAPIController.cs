using First_core_project.Helpers;
using First_core_project.Models;
using First_core_project.DTOs.API; // الفولدر الجديد للـ DTOs
using First_core_project.Services.API; // الفولدر الجديد للسيرفس
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace First_core_project.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        private readonly IApiProductService _productService;

        // بننادي السيرفس الجديدة مش الـ Context
        public ProductsAPIController(IApiProductService productService)
        {
            _productService = productService;
        }

        // GET: /api/ProductsAPI
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? categoryId = null,
            [FromQuery] string? search = null,
            [FromQuery] string? sort = null
        )
        {
            // تجهيز الـ BaseUrl للصور
            var baseUrl = $"{Request.Scheme}://{Request.Host}/Images/";

            // بنطلب البيانات من السيرفس
            var (products, totalCount) = await _productService.GetAllProductsAsync(page, pageSize, categoryId, search, sort, baseUrl);

            // الرد الموحد "العظمة"
            var response = new ApiResponse<object>(true, "تم جلب المنتجات بنجاح", new
            {
                page,
                pageSize,
                totalCount,
                items = products
            });

            return Ok(response);
        }

        // POST: api/ProductsAPI
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Super Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>(false, "البيانات غير صالحة", ModelState));
            }

            var productId = await _productService.CreateProductAsync(product);

            return Ok(new ApiResponse<object>(true, "تمت إضافة المنتج بنجاح", new { productId }));
        }
    }
}