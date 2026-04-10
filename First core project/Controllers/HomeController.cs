using First_core_project.Dtos;
using First_core_project.Helpers;
using First_core_project.Models;
using First_core_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace First_core_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IHomeService homeService,
            ICartService cartService,
            IProductService productService,
            IReviewService reviewService,
            ILogger<HomeController> logger)
        {
            _homeService = homeService;
            _cartService = cartService;
            _productService = productService;
            _reviewService = reviewService;
            _logger = logger;
        }

       
        // Home
     

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Home page loaded");

            var result = await _homeService.GetHomeDataAsync();
            return View(result);
        }

        
        // Cart
       

        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _logger.LogInformation("Cart viewed by user {UserId}", userId);

            var myCart = await _cartService.GetCartItemsAsync(userId);
            return View(myCart);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _logger.LogInformation("User {UserId} adding product {ProductId} to cart", userId, id);

            await _cartService.AddToCartAsync(id, userId);
            return RedirectToAction("Cart");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, string operation)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartService.UpdateQuantityAsync(id, userId, operation);

            if (!result.Success)
            {
                _logger.LogWarning("Failed quantity update for product {ProductId} by user {UserId}", id, userId);
                return Json(new { success = false });
            }

            return Json(new
            {
                success = true,
                newQty = result.NewQty,
                itemTotal = result.ItemTotal.ToString("N2"),
                cartTotal = result.CartTotal.ToString("N2")
            });
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _logger.LogInformation("User {UserId} removed product {ProductId} from cart", userId, id);

            await _cartService.RemoveFromCartAsync(id, userId);
            return RedirectToAction("Cart");
        }

        // =======================
        // Categories & Products
        // =======================

        public async Task<IActionResult> Categories()
        {
            var categories = await _productService.GetCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Products(int id, [FromQuery] PaginationParams param)
        {
            _logger.LogInformation("Products page loaded for category {CategoryId}", id);

            var products = await _productService.GetProductsByCategoryAsync(id, param);

            ViewBag.CategoryId = id;
            ViewBag.SortBy = param.SortBy;
            ViewBag.Desc = param.Desc;
            ViewBag.PageNumber = param.PageNumber;

            return View(products);
        }

        public async Task<IActionResult> CurrentProducts(int id)
        {
            var product = await _productService.GetProductDetailsAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Product details requested but not found. ProductId: {ProductId}", id);
                return NotFound();
            }

            return View(product);
        }

        // =======================
        // Search
        // =======================

        [HttpGet]
        public async Task<IActionResult> Search(string? xname, [FromQuery] PaginationParams param)
        {
            _logger.LogInformation("Search requested for keyword: {Keyword}", xname);

            var products = await _productService.SearchProductsAsync(xname, param);

            ViewBag.Keyword = xname;
            ViewBag.CurrentPage = param.PageNumber;
            return View(products);
        }

        // =======================
        // Reviews
        // =======================

        [HttpPost]
        public async Task<IActionResult> SendReview(ReviewVm model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid review submission by: {User}", model?.Name);
                return RedirectToAction("Index");
            }

            await _reviewService.AddReviewAsync(model);

            _logger.LogInformation("New review submitted by: {User}", model.Name);

            return RedirectToAction("Index");
        }

        // =======================
       

       

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}