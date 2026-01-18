using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DenemeDers.Controllers
{
    [Authorize(Roles = "admin,memur")]
    public class OgrenciController : Controller
    {
        private readonly ContextDb _context;

        public OgrenciController(ContextDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var ogrenciler = _context.Ogrenciler.Include(x => x.Bolum).ToList();
            return View(ogrenciler);
        }

        [HttpGet]
        public IActionResult Kayit()
        {
            ViewBag.Bolumler = new SelectList(_context.Bolumler.ToList(), "BolumId", "BolumAdi");
            return View();
        }

        [HttpPost]
        public IActionResult Kayit(Ogrenci model)
        {
            ModelState.Remove("AppUser");
            ModelState.Remove("Bolum");

            var varMi = _context.Ogrenciler.Any(x => x.OgrNo == model.OgrNo);
            if (varMi)
            {
                ViewBag.Bolumler = new SelectList(_context.Bolumler, "BolumId", "BolumAdi");
                ModelState.AddModelError("OgrNo", "Bu öğrenci numarası zaten sisteme kayıtlı.");
            }

            if (ModelState.IsValid)
            {
                string hamSifre = "123";
                string hashliSifre = BCrypt.Net.BCrypt.HashPassword(hamSifre);
                var hesap = new AppUser
                {
                    AdSoyad = model.Ad + " " + model.Soyad,
                    KullaniciAdi = model.OgrNo,
                    Sifre = hashliSifre, 
                    Rol = "Ogrenci"
                };

                _context.AppUsers.Add(hesap);
                _context.SaveChanges();

                model.AppUserId = hesap.AppUserId;
                _context.Ogrenciler.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Bolumler = new SelectList(_context.Bolumler.ToList(), "BolumId", "BolumAdi");
            return View(model);
        }

        public IActionResult Sil(int id)
        {
            var ogrenci = _context.Ogrenciler.Find(id);
            if (ogrenci != null)
            {
                var user = _context.AppUsers.Find(ogrenci.AppUserId);
                _context.Ogrenciler.Remove(ogrenci);
                if (user != null)
                {
                    _context.AppUsers.Remove(user);
                }

                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var ogrenci = _context.Ogrenciler.Find(id);
            if (ogrenci == null) return NotFound();

            ViewBag.Bolumler = new SelectList(_context.Bolumler.ToList(), "BolumId", "BolumAdi", ogrenci.BolumId);
            return View(ogrenci);
        }

        [HttpPost]
        public IActionResult Guncelle(Ogrenci model)
        {
            var mevcutOgrenci = _context.Ogrenciler
                                        .Include(x => x.AppUser)
                                        .FirstOrDefault(x => x.OgrenciId == model.OgrenciId);

            if (mevcutOgrenci == null) return NotFound();

            mevcutOgrenci.Ad = model.Ad;
            mevcutOgrenci.Soyad = model.Soyad;
            mevcutOgrenci.OgrNo = model.OgrNo;
            mevcutOgrenci.BolumId = model.BolumId;
            mevcutOgrenci.Sinif = model.Sinif;
            mevcutOgrenci.Yas = model.Yas;

            if (mevcutOgrenci.AppUser != null)
            {
                mevcutOgrenci.AppUser.AdSoyad = model.Ad + " " + model.Soyad;
                mevcutOgrenci.AppUser.KullaniciAdi = model.OgrNo;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}