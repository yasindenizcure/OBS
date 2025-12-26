using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DenemeDers.Controllers
{
    [Authorize(Roles = "hoca")]
    public class NotController : Controller
    {
        private readonly ContextDb _context;

        public NotController(ContextDb context)
        {
            _context = context;
        }

        public IActionResult NotIndex()
        {
            var notlar = _context.Notlar
                .Include(x => x.Ogrenci) 
                .Include(x => x.OgretimGorevlisi) 
                .Include(x => x.Ders) 
                .ToList();
            return View(notlar);
        }

        [HttpGet]
        public IActionResult NotEkle()
        {
            var dersler = _context.Dersler.Include(d => d.OgretimGorevlisi).ToList();
            ViewBag.DersHocaListesi = dersler.Select(d => new {
                dersId = d.DersId,
                hocaId = d.OgretimGorevlisiId,
                hocaAdSoyad = d.OgretimGorevlisi != null
                              ? d.OgretimGorevlisi.Ad + " " + d.OgretimGorevlisi.Soyad
                              : "Hoca Atanmamış"
            }).ToList();

            ViewBag.Ogrenciler = new SelectList(_context.Ogrenciler
                .Select(s => new { Id = s.OgrenciId, Ad = s.Ad + " " + s.Soyad }), "Id", "Ad");

            ViewBag.Dersler = new SelectList(dersler, "DersId", "DersAdi");

            return View();
        }

        [HttpPost]
        public IActionResult NotEkle(Not notlar)
        {
            _context.Notlar.Add(notlar);
            _context.SaveChanges();
            return RedirectToAction("NotIndex", "Not");
        }

        public IActionResult NotSil(int id)
        {
            var deger = _context.Notlar.Find(id);
            if (deger != null)
            {
                _context.Notlar.Remove(deger);
                _context.SaveChanges();
            }
            return RedirectToAction("NotIndex", "Not");
        }

        [HttpGet]
        public IActionResult NotGuncelle(int id)
        {
            var deger = _context.Notlar.Find(id);
            return View(deger);
        }

        [HttpPost]
        public IActionResult NotGuncelle(Not notlar)
        {
            _context.Notlar.Update(notlar);
            _context.SaveChanges();
            return RedirectToAction("NotIndex", "Not");
        }
    }
}