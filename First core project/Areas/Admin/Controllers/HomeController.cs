using First_core_project.Data;   // تأكد أن هذا المسار يودي لمجلد الداتا اللي فيه الـ Context
using First_core_project.Models; // تأكد أن هذا المسار يودي لمجلد الموديلز عندك
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace First_core_project.Areas.Admin.Controllers
{
    [Area("Admin")] 
   
    public class HomeController : Controller
    {
       
        private readonly SouqcomContext _context;

        public HomeController(SouqcomContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            ViewBag.ProductsCount = _context.Products.Count();
            ViewBag.CategoriesCount = _context.Categories.Count();
            ViewBag.ReviewsCount = _context.Reviews.Count();
            ViewBag.ActiveCarts = _context.Carts
         .Select(c => c.UserId)
         .Distinct()
         .Count();

            return View();


        }
    }
}