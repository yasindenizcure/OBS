using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var ogrenciler = _context.Ogrenciler.ToList();
            return View(ogrenciler);
        }

        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Kayit(Ogrenci model)
        {
            var hesap = new AppUser
            {
                AdSoyad = model.Ad + " " + model.Soyad,
                KullaniciAdi = model.OgrNo,
                Sifre = "123",
                Rol = "Ogrenci"
            };

            _context.AppUsers.Add(hesap);
            _context.SaveChanges();

            model.AppUserId = hesap.AppUserId;

            _context.Ogrenciler.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Sil(int id)
        {
            var ogrenci = _context.Ogrenciler.Find(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Guncelle(int id)
        {

            var ogrenci = _context.Ogrenciler.Find(id);

            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        [HttpPost]
        public IActionResult Guncelle(Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                _context.Ogrenciler.Update(ogrenci); 
                _context.SaveChanges(); 
                return RedirectToAction("Index"); 
            }

            return View(ogrenci);
        }
    }
}