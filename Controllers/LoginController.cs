using DenemeDers.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DenemeDers.Controllers
{
    public class LoginController : Controller
    {
        private readonly ContextDb _context;

        public LoginController(ContextDb context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string ad, string sifre)
        {
            var user = _context.AppUsers.FirstOrDefault(x => x.KullaniciAdi == ad && x.Sifre == sifre);

            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.AdSoyad),
            new Claim(ClaimTypes.Role, user.Rol),
            new Claim("UserId", user.AppUserId.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                string userRole = user.Rol.ToLower().Trim();

                if (userRole == "hoca")
                {
                    return RedirectToAction("NotIndex", "Not");
                }
                else if (userRole == "admin" || userRole == "memur")
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                if (userRole == "ogrenci")
                {
                    return RedirectToAction("Notlarim", "OgrenciPanel");
                }
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
