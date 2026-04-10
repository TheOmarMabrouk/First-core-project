using First_core_project.Data;
using First_core_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace First_core_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
          
            return View();
        }

        // الصفحة الأساسية لعرض المستخدمين وصلاحياتهم
        public IActionResult UserRoles()
        {
            var users = _userManager.Users.ToList();
            ViewBag.allRoles = _roleManager.Roles.ToList();
            return View(users);
        }

        // الأكشن السحري: إضافة أو حذف الصلاحية بضغطة واحدة
        [HttpPost]
        public async Task<IActionResult> ToggleUserRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                // لو الصلاحية موجودة.. احذفها
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
            else
            {
                // لو مش موجودة.. ضيفها
                await _userManager.AddToRoleAsync(user, roleName);
            }

            return RedirectToAction(nameof(UserRoles));
        }

        // أكشن إضافة صلاحية جديدة للسيستم (مثل: Editor, Moderator)
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
            return RedirectToAction(nameof(Role));
        }

        public IActionResult Role()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult User()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}