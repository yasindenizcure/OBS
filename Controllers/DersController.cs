using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DenemeDers.Controllers
{
    [Authorize(Roles = "admin,memur")]
    public class DersController : Controller
    {
        private readonly ContextDb _context;

        public DersController(ContextDb context)
        {
            _context = context;
        }
        private void OgretmenListesiniDoldur()
        {
            List<SelectListItem> ogretmenListesi = (from x in _context.OgretimGorevlileri.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Ad + " " + x.Soyad,
                                                        Value = x.OgretimGorevlisiId.ToString() 
                                                    }).ToList();
            ViewBag.ogrGorevlisi = ogretmenListesi;
            ModelState.Remove("OgretimGorevlisi");
        }
        public IActionResult Dersler()
        {
            var dersListesi = _context.Dersler.Include(x=>x.OgretimGorevlisi).ToList();
            return View(dersListesi);
        }
        public IActionResult DersKayit()
        {
            OgretmenListesiniDoldur();
            return View();
        }
        [HttpPost]
        public IActionResult DersKayit(Ders dersler)
        {
            List<SelectListItem> ogretmenListesi = (from x in _context.OgretimGorevlileri.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Ad + " " + x.Soyad,
                                                        Value = x.OgretimGorevlisiId.ToString()
                                                    }).ToList();
            ViewBag.ogrGorevlisi = ogretmenListesi;
            ModelState.Remove("OgretimGorevlisi");

            if (ModelState.IsValid)
            {
                _context.Dersler.Add(dersler);
                _context.SaveChanges();
                return RedirectToAction("Dersler");
            }

            return View(dersler);
        }
        public IActionResult DersSil(int id) 
        {
            var ders = _context.Dersler.Find(id);
            if(ders != null)
            {
                _context.Dersler.Remove(ders);
                _context.SaveChanges();
            }
            return RedirectToAction("Dersler");
        }
        [HttpGet]
        public IActionResult DersGuncelle(int id) 
        {
            var ders = _context.Dersler.Find(id);
            if(ders == null)
            {
                return RedirectToAction("Dersler");
            }
            OgretmenListesiniDoldur();
            return View(ders);
        }
        [HttpPost]
        public IActionResult DersGuncelle(Ders dersler) 
        {
            var deger = _context.Dersler.Find(dersler.DersId);
            if (deger != null)
            {
                deger.DersAdi = dersler.DersAdi;
                deger.SinifDuzeyi = dersler.SinifDuzeyi;
                deger.DersTuru = dersler.DersTuru;
                deger.OgretimGorevlisiId = dersler.OgretimGorevlisiId;
                _context.SaveChanges();
            }
           return RedirectToAction("Dersler");
        }
    }
}
