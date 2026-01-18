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
        public IActionResult NotEkle(int? dersId)
        {
            int hocaId = GetAktifHocaId();
            var hoca = _context.OgretimGorevlileri.FirstOrDefault(x => x.OgretimGorevlisiId == hocaId);

            // Hocanın girdiği dersler
            var dersler = _context.Dersler.Where(x => x.OgretimGorevlisiId == hocaId).ToList();
            ViewBag.Dersler = new SelectList(dersler, "DersId", "DersAdi", dersId);

            if (dersId.HasValue)
            {
                var secilenDers = _context.Dersler.Find(dersId.Value);
                var bolumundekiOgrenciler = _context.Ogrenciler
                    .Where(x => x.BolumId == secilenDers.BolumId) // Dersin bölümü = Öğrencinin bölümü
                    .Select(s => new { Id = s.OgrenciId, Ad = s.Ad + " " + s.Soyad })
                    .ToList();

                ViewBag.Ogrenciler = new SelectList(bolumundekiOgrenciler, "Id", "Ad");
            }
            else
            {
                // Ders seçilmemişse liste boş kalsın
                ViewBag.Ogrenciler = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            ViewBag.Unvan = hoca?.Unvan;
            ViewBag.HocaAdSoyad = hoca?.Ad + " " + hoca?.Soyad;
            ViewBag.HocaId = hocaId;
            return View();
        }

        [HttpPost]
        public IActionResult NotEkle(Not model)
        {
            ModelState.Remove("Ogrenci");
            ModelState.Remove("Ders");
            ModelState.Remove("OgretimGorevlisi");

            var varmi = _context.Notlar.Any(x => x.OgrenciId == model.OgrenciId && x.DersId == model.DersId);
            if (varmi)
            {
                TempData["ErrorMessage"] = "SİSTEM UYARISI: Bu öğrenciye bu dersten daha önce not girilmiş!";
                NotListeleriniDoldur(model);
                return View(model);
            }

            var secilenDers = _context.Dersler.Find(model.DersId);
            var secilenOgrenci = _context.Ogrenciler.Find(model.OgrenciId);

            if (secilenDers != null && secilenOgrenci != null)
            {
                if (secilenDers.BolumId != secilenOgrenci.BolumId)
                {
                    TempData["ErrorMessage"] = "PROTOKOL HATASI: Öğrencinin bölümü ile dersin bölümü uyuşmuyor!";
                    NotListeleriniDoldur(model);
                    return View(model);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Notlar.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("NotIndex");
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "KRİTİK HATA: Veritabanı kayıt protokolü başarısız!";
                }
            }

            NotListeleriniDoldur(model);
            return View(model);
        }

        private void NotListeleriniDoldur(Not model)
        {
            int hocaId = GetAktifHocaId();
            var hoca = _context.OgretimGorevlileri.Find(hocaId);

            ViewBag.HocaId = hocaId;
            ViewBag.HocaAdSoyad = hoca?.Ad + " " + hoca?.Soyad;
            var tumOgrenciler = _context.Ogrenciler
                .Select(s => new { Id = s.OgrenciId, Ad = s.Ad + " " + s.Soyad })
                .ToList();

            ViewBag.Ogrenciler = new SelectList(tumOgrenciler, "Id", "Ad", model.OgrenciId);
            ViewBag.Dersler = new SelectList(_context.Dersler.Where(x => x.OgretimGorevlisiId == hocaId).ToList(), "DersId", "DersAdi", model.DersId);
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