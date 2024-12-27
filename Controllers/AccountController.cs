using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SalonCruella.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF koruması
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "E-posta ve şifre boş olamaz.";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (result.Succeeded)
            {
                // Kullanıcının rollerini kontrol et
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                {
                    // Admin rolü için yönlendirme
                    return RedirectToAction("Index", "Admin");
                }
                else if (roles.Contains("User"))
                {
                    // User rolü için yönlendirme
                    return RedirectToAction("RandevuAl", "User");
                }

                // Varsayılan yönlendirme
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Giriş başarısız. Bilgilerinizi kontrol edin.";
            return View();
        }

        // GET: Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        // GET: Access Denied (Yetkisiz erişim)
        public IActionResult AccessDenied()
        {
            // RandevuAl sayfasına yönlendirme
            return RedirectToAction("RandevuAl", "User");
        }

    }
}
