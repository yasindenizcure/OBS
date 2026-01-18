using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenemeDers.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ContextDb _context;

        public DashboardController(ContextDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sonOgrenciler = _context.Ogrenciler
        .OrderByDescending(x => x.OgrenciId)
        .Take(2)
        .Select(x => new { Metin = $"YENİ ÖĞRENCİ: {x.Ad} {x.Soyad}", Tarih = DateTime.Now })
        .ToList();

            var sonHocalar = _context.OgretimGorevlileri
                .OrderByDescending(x => x.OgretimGorevlisiId)
                .Take(2)
                .Select(x => new { Metin = $"YENİ PERSONEL: {x.Ad} {x.Soyad}", Tarih = DateTime.Now })
                .ToList();

            var sonDers = _context.Dersler
                .OrderByDescending(x => x.DersId)
                .Take(1)
                .Select(x => new { Metin = $"MÜFREDAT GÜNCELLEME: {x.DersAdi}", Tarih = DateTime.Now })
                .ToList();

            ViewBag.SonIslemler = sonOgrenciler.Concat(sonHocalar).Concat(sonDers).ToList();

            var dersAnaliz = _context.Notlar
         .Select(x => new {
             x.Ders.DersAdi,
             x.VizeNotu,
             x.FinalNotu
         })
         .AsEnumerable()
         .GroupBy(x => x.DersAdi)
         .Select(g => new {
             DersAdi = g.Key,
             Ortalama = g.Average(a => (a.VizeNotu * 0.4) + (a.FinalNotu * 0.6))
         })
         .Take(5)
         .ToList();
            ViewBag.DersAdlari = dersAnaliz.Select(x=>x.DersAdi).ToArray();
            ViewBag.DersOrtalamalari = dersAnaliz.Select(x=>x.Ortalama).ToArray();
            ViewBag.OgrenciSayisi = _context.Ogrenciler.Count();
            ViewBag.HocaSayisi = _context.OgretimGorevlileri.Count();
            ViewBag.DersSayisi = _context.Dersler.Count();
            ViewBag.BolumSayisi = _context.Bolumler.Count();
            ViewBag.Duyurular = _context.Duyurular.OrderByDescending(x => x.Tarih).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult DuyuruYayinla()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DuyuruYayinla(Duyuru model)
        {
            if (model.Icerik != null)
            {
                _context.Duyurular.Add(model);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
