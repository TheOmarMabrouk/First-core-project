using First_core_project.Data;
using First_core_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;                                               

namespace First_core_project.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ApplicationDbContext _identityDb;
        private readonly SouqcomContext db;

        public HomeController(
            
            ApplicationDbContext identityDb,
            SouqcomContext DB)
        {
            
            _identityDb = identityDb;
            db = DB;
        }

      
      

        public IActionResult Index()
        {
            IndexVm result = new IndexVm();

            result.Categories = db.Categories.ToList();
            result.Products = db.Products.ToList();
            result.Reviews = db.Reviews.ToList();
            result.LatestProducts = db.Products.OrderByDescending(x => x.EntryDate).Take(3).ToList();


            return View(result);
        }
        [Authorize]
        public IActionResult Cart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var myCart = db.Carts
                           .Include(x => x.Product)
                           .Where(x => x.UserId == userId)
                           .ToList();

            return View(myCart);
        }

        // 2. ????? ???? ????? (???? ????? ???? ???????)
        [Authorize]
        public IActionResult AddToCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // ????? ?? ?????? ?? ????? ????? ?? ??? ???????? ???
            var existingItem = db.Carts.FirstOrDefault(c => c.ProductId == id && c.UserId == userId);

            if (existingItem != null)
            {
                // ?? ????? ??? ?????? ??
                existingItem.Qty++;
            }
            else
            {
                // ?? ?? ????? ??? ??? ????
                var cartItem = new Cart
                {
                    ProductId = id,
                    UserId = userId,
                    Qty = 1
                };
                db.Carts.Add(cartItem);
            }

            db.SaveChanges();
            return RedirectToAction("Cart");
        }

        // 3. ????? ?????? (?????? ???? ????? ????? - API Style)
        [Authorize]
        [HttpPost]
        public IActionResult UpdateQuantity(int id, string operation)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // ??? ???? Include ???? ?????
            var item = db.Carts
                         .Include(x => x.Product)
                         .FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (item == null) return NotFound();

            if (operation == "increase")
            {
                item.Qty = (item.Qty ?? 0) + 1;
            }
            else if (operation == "decrease")
            {
                if (item.Qty > 1)
                {
                    item.Qty -= 1;
                }
                else
                {
                    db.Carts.Remove(item);
                    db.SaveChanges();

                    return Ok(new
                    {
                        itemDeleted = true,
                        cartTotal = GetCartTotal(userId)
                    });
                }
            }

            db.SaveChanges();

            return Ok(new
            {
                itemDeleted = false,
                newQty = item.Qty,
                itemTotal = ((item.Qty ?? 0) * (item.Product?.Price ?? 0)).ToString("N2"),
                cartTotal = GetCartTotal(userId)
            });
        }

        // 4. ??? ???? ???????
        [Authorize]
        public IActionResult RemoveFromCart(int id)
        {
            var item = db.Carts.Find(id);
            if (item != null)
            {
                db.Carts.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Cart");
        }

        // ???? ?????? ????? ?????? ????? ????? ????? ?????
        private string GetCartTotal(string userId)
        {
            var total = db.Carts
                          .Where(x => x.UserId == userId)
                          .Sum(x => (x.Qty ?? 0) * (x.Product.Price ?? 0));
            return total.ToString("N2");
        }


        public IActionResult Categories() => View(db.Categories.ToList());

        public IActionResult Products(int id)
        {
            var products = db.Products.Where(x => x.Catid == id).ToList();
            return View(products);
        }

        public IActionResult CurrentProducts(int id)
        {
            var product = db.Products
                .Include(x => x.Cat)
                .Include(x => x.ProductImages)
                .FirstOrDefault(x => x.Id == id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Search(string xname)
        {
            var products =  new List<Product>();

            if (string.IsNullOrEmpty(xname))
                products = db.Products.ToList();

            else
               products =  db.Products.Where(x => x.Name.Contains(xname)).ToList();

            return View(products);
        }

        [HttpPost]
        public IActionResult SendReview(ReviewVm model)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(new Review
                {
                    Name = model.Name,
                    Email = model.Email,
                    Subject = model.Subject,
                    Description = model.Description
                });
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Privacy() => View();
    }
}