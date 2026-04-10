using First_core_project.Models;
using First_core_project.Helpers; // تأكد من استدعاء فولدر الهيلبرز
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace First_core_project.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAPIController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AccountAPIController(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVm model)
        {
            // 1. التأكد من وجود اليوزر
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // 2. جلب الصلاحيات (Roles) عشان نحطها جوه التوكن
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                // 3. إنشاء التوكن باستخدام الـ Key اللي حطيناه في appsettings
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                // التعديل هنا: الرد بقى منظم جوه ApiResponse
                var responseData = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userName = user.UserName
                };

                return Ok(new ApiResponse<object>(true, "تم تسجيل الدخول بنجاح", responseData));
            }

            // التعديل هنا: الرد في حالة الفشل بقى منظم برضه
            return Unauthorized(new ApiResponse<object>(false, "البريد الإلكتروني أو كلمة المرور غير صحيحة", null));
        }
    }
}