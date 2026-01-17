using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DenemeDers.Controllers
{
    [Authorize(Roles = "admin,memur")]
    public class OgretimGorevlisiController : Controller
    {
        private readonly ContextDb _context;

        public OgretimGorevlisiController(ContextDb context)
        {
            _context = context;
        }
        public IActionResult OgrGorevlisi()
        {
            var ogrgorevlisi = _context.OgretimGorevlileri.Include(x => x.AppUser).ToList();
            return View(ogrgorevlisi);
        }

        [HttpGet]
        public IActionResult OgrGorevlisiKayit()
        {
            var bolumler = _context.Bolumler.ToList();
            ViewBag.BolumListesi = new SelectList(bolumler, "BolumId", "BolumAdi");
            return View();
        }

        [HttpPost]
        public IActionResult OgrGorevlisiKayit(OgretimGorevlisi model)
        {
            ModelState.Remove("Dersler");
            ModelState.Remove("AppUser");
            ModelState.Remove("Bolum");

            if (ModelState.IsValid)
            {
                string hamSifre = "123";
                string hashliSifre = BCrypt.Net.BCrypt.HashPassword(hamSifre);

                var yeniKullanici = new AppUser
                {
                    AdSoyad = model.Ad + " " + model.Soyad,
                    KullaniciAdi = (model.Ad.ToLower() + "." + model.Soyad.ToLower()).Replace(" ", ""),
                    Sifre = hashliSifre,
                    Rol = "Hoca"
                };

                _context.AppUsers.Add(yeniKullanici);
                _context.SaveChanges();

                model.AppUserId = yeniKullanici.AppUserId;
                _context.OgretimGorevlileri.Add(model);
                _context.SaveChanges();

                return RedirectToAction("OgrGorevlisi");
            }
            return View(model);
        }

        public IActionResult OgrGorevlisiSil(int id)
        {
            var hoca = _context.OgretimGorevlileri.Find(id);
            if (hoca == null) return NotFound();

            var user = _context.AppUsers.Find(hoca.AppUserId);

            _context.OgretimGorevlileri.Remove(hoca);
            if (user != null) _context.AppUsers.Remove(user);

            _context.SaveChanges();
            return RedirectToAction("OgrGorevlisi");
        }

        [HttpGet]
        public IActionResult OgrGorevlisiGuncelle(int id)
        {
            var hoca = _context.OgretimGorevlileri.Find(id);
            if (hoca == null) return NotFound();

            ViewBag.BolumListesi = new SelectList(_context.Bolumler.ToList(), "BolumId", "BolumAdi", hoca.BolumId);

            return View(hoca);
        }
        [HttpPost]
        public IActionResult OgrGorevlisiGuncelle(OgretimGorevlisi model)
        {
            var guncellenecekHoca = _context.OgretimGorevlileri
                                           .Include(x => x.AppUser)
                                           .FirstOrDefault(x => x.OgretimGorevlisiId == model.OgretimGorevlisiId);

            if (guncellenecekHoca == null)
            {
                return NotFound();
            }
            guncellenecekHoca.Ad = model.Ad;
            guncellenecekHoca.Soyad = model.Soyad;
            guncellenecekHoca.Unvan = model.Unvan;
            guncellenecekHoca.Bolum = model.Bolum;

            if (guncellenecekHoca.AppUser != null)
            {
                guncellenecekHoca.AppUser.AdSoyad = model.Ad + " " + model.Soyad;
            }

            try
            {
                _context.SaveChanges();
                return RedirectToAction("OgrGorevlisi");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Güncelleme sırasında hata oluştu: " + ex.Message);
                return View(model);
            }

        }
        [HttpGet]
        public IActionResult SifreleriTopluHashle()
        {
            var tumKullanicilar = _context.AppUsers.ToList();
            int sayac = 0;
            foreach (var user in tumKullanicilar)
            {
                if (user.Sifre.Length < 20)
                {
                    user.Sifre = BCrypt.Net.BCrypt.HashPassword(user.Sifre);
                    sayac++;
                }
            }
            _context.SaveChanges();
            return Content($"{sayac} adet kullanıcının şifresi başarıyla hashlendi!");
        }
    }
}