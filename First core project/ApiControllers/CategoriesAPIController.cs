using First_core_project.Helpers;
using First_core_project.Services.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace First_core_project.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesAPIController : ControllerBase
    {
        private readonly IApiCategoryService _categoryService;

        public CategoriesAPIController(IApiCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // جلب كل الأقسام
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(new ApiResponse<object>(true, "تم جلب الأقسام بنجاح", categories));
        }

        // إضافة قسم جديد (للسوبر أدمن فقط)
        [Authorize(Roles = "Super Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest(new ApiResponse<object>(false, "الاسم مطلوب", null));

            var categoryId = await _categoryService.CreateCategoryAsync(name);
            return Ok(new ApiResponse<object>(true, "تمت إضافة القسم بنجاح", new { categoryId }));
        }
    }
}