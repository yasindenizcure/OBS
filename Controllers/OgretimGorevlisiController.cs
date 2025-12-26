using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // Listeleme
        public IActionResult OgrGorevlisi()
        {
            var ogrgorevlisi = _context.OgretimGorevlileri.Include(x => x.AppUser).ToList();
            return View(ogrgorevlisi);
        }

        [HttpGet]
        public IActionResult OgrGorevlisiKayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OgrGorevlisiKayit(OgretimGorevlisi model)
        {
            ModelState.Remove("Dersler");
            ModelState.Remove("AppUser");

            if (ModelState.IsValid)
            {
                var yeniKullanici = new AppUser
                {
                    AdSoyad = model.Ad + " " + model.Soyad,
                    KullaniciAdi = (model.Ad.ToLower() + "." + model.Soyad.ToLower()).Replace(" ", ""),
                    Sifre = "123", 
                    Rol = "Hoca"
                };

                _context.AppUsers.Add(yeniKullanici);
                _context.SaveChanges();

                // Hocayı bu kullanıcı hesabına bağlıyoruz
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
            var ogrgorevlisi = _context.OgretimGorevlileri.Find(id);
            if (ogrgorevlisi == null) return NotFound();
            return View(ogrgorevlisi);
        }

        [HttpPost]
        public IActionResult OgrGorevlisiGuncelle(OgretimGorevlisi model)
        {
            _context.OgretimGorevlileri.Update(model);
            _context.SaveChanges();
            return RedirectToAction("OgrGorevlisi");
        }
    }
}