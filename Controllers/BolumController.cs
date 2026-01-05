using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DenemeDers.Context;
using DenemeDers.Entity;

namespace DenemeDers.Controllers
{
    [Authorize(Roles="admin,Admin")]
    public class BolumController : Controller
    {
        private readonly ContextDb _context;

        public BolumController(ContextDb context)
        {
            _context = context;
        }

        public IActionResult BolumListesi()
        {
            var values = _context.Bolumler.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult BolumEkle() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult BolumEkle(Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Bolumler.Add(bolum);
                    _context.SaveChanges();
                    return RedirectToAction("BolumListesi");
                }
                catch (Exception ex)
                {
                    // Veritabanı hatası alırsan buraya düşer
                    ModelState.AddModelError("", "Veritabanına kaydedilirken hata oluştu.");
                }
            }
            return View(bolum);
        }
        public IActionResult BolumSil(int id) 
        {
            var value = _context.Bolumler.Find(id);
            if (value != null) 
            {
                _context.Bolumler.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("BolumListesi");
        }
        [HttpGet]
        public IActionResult BolumGuncelle(int id) 
        {
            var bolum = _context.Bolumler.Find(id);
            if (bolum == null) 
            {
                return RedirectToAction("BolumListesi");
            }
            return View(bolum);
        }
        [HttpPost]
        public IActionResult BolumGuncelle(Bolum bolum) 
        {
            var bolumler = _context.Bolumler.Find(bolum.BolumId);
            if (bolumler != null) 
            {
                bolumler.BolumAdi = bolum.BolumAdi;
                _context.SaveChanges();
                return RedirectToAction("BolumListesi");
            }
            return View(bolum);
        }
    }
}
