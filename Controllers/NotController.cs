using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DenemeDers.Controllers
{
    public class NotController : Controller
    {
        private readonly ContextDb _context;

        public NotController(ContextDb context)
        {
            _context = context;
        }

        private int GetAktifHocaId()
        {
            return HttpContext.Session.GetInt32("HocaId") ?? 0;
        }

        public IActionResult NotIndex()
        {
            int hocaId = GetAktifHocaId();
            var notlar = _context.Notlar
                .Include(x => x.Ogrenci)
                .Include(x => x.Ders)
                .Include(x => x.OgretimGorevlisi)
                .Where(x => x.OgretimGorevlisiId == hocaId)
                .ToList();
            return View(notlar);
        }
        public IActionResult NotSil(int id)
        {
            var deger = _context.Notlar.Find(id);

            if (deger != null)
            {
                _context.Notlar.Remove(deger);
                _context.SaveChanges();
            }

            return RedirectToAction("NotIndex");
        }
        [HttpGet]
        public IActionResult NotEkle()
        {
            int hocaId = GetAktifHocaId();
            var hoca = _context.OgretimGorevlileri.FirstOrDefault(x => x.OgretimGorevlisiId == hocaId);
            if (hoca == null) return Content("Hoca oturumu bulunamadı! Lütfen tekrar giriş yapın.");

            var dersler = _context.Dersler.Where(x => x.OgretimGorevlisiId == hocaId).ToList();

            ViewBag.HocaAdSoyad = hoca.Ad + " " + hoca.Soyad;
            ViewBag.HocaId = hocaId;

            ViewBag.Ogrenciler = new SelectList(_context.Ogrenciler
                .Select(s => new { Id = s.OgrenciId, Ad = s.Ad + " " + s.Soyad }), "Id", "Ad");

            ViewBag.Dersler = new SelectList(dersler, "DersId", "DersAdi");

            return View();
        }

        [HttpPost]
        public IActionResult NotEkle(Not model)
        {
            if (model.OgretimGorevlisiId == 0)
            {
                return Content("Hata: Hoca ID gönderilemedi!");
            }

            try
            {
                _context.Notlar.Add(model);
                _context.SaveChanges();
                return RedirectToAction("NotIndex");
            }
            catch (System.Exception)
            {
                return Content("Veritabanı hatası: Seçtiğiniz hoca veya ders geçersiz!");
            }

        }
        [HttpGet]
        public IActionResult NotGuncelle(int id)
        {
            var deger = _context.Notlar
                .Include(x => x.Ogrenci)
                .Include(x => x.Ders)
                .FirstOrDefault(x => x.NotId == id); 

            if (deger == null) return NotFound();

            var hoca = _context.OgretimGorevlileri.Find(deger.OgretimGorevlisiId);
            ViewBag.HocaAdSoyad = hoca.Ad + " " + hoca.Soyad;

            return View(deger);
        }

        [HttpPost]
        public IActionResult NotGuncelle(Not model)
        {
            var mevcutNot = _context.Notlar.Find(model.NotId);

            if (mevcutNot != null)
            {
                mevcutNot.VizeNotu = model.VizeNotu;
                mevcutNot.FinalNotu = model.FinalNotu;

                _context.SaveChanges();
            }

            return RedirectToAction("NotIndex");
        }
    }
}