using DenemeDers.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BCrypt.Net;

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
            if (User.Identity.IsAuthenticated)
            {
                return Yonlendir(User.FindFirst(ClaimTypes.Role)?.Value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string ad, string sifre)
        {
            var user = _context.AppUsers.FirstOrDefault(x => x.KullaniciAdi == ad);

            if (user != null)
            {
                bool isValid = false;
                try
                {
                    isValid = BCrypt.Net.BCrypt.Verify(sifre, user.Sifre);
                }
                catch
                {
                    isValid = (sifre == user.Sifre);
                }

                if (isValid)
                {
                    var hoca = _context.OgretimGorevlileri.FirstOrDefault(x => x.AppUserId == user.AppUserId);
                    if (hoca != null)
                    {
                        HttpContext.Session.SetInt32("HocaId", hoca.OgretimGorevlisiId);
                    }

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.AdSoyad),
                new Claim(ClaimTypes.Role, user.Rol.Trim()),
                new Claim("UserId", user.AppUserId.ToString()),
                new Claim("OgrNo", user.KullaniciAdi)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return Yonlendir(user.Rol);
                }
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }
        private IActionResult Yonlendir(string role)
        {
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Index");

            string userRole = role.ToLower().Trim();

            if (userRole == "admin" || userRole == "memur")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else if (userRole == "hoca")
            {
                return RedirectToAction("NotIndex", "Not");
            }
            else if (userRole == "ogrenci")
            {
                return RedirectToAction("Notlarim", "OgrenciPanel");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}