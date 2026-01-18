using DenemeDers.Context;
using DenemeDers.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DenemeDers.Controllers
{
    [Authorize(Roles = "admin")]
    public class KullaniciController : Controller
    {
        private readonly ContextDb _context;

        public KullaniciController(ContextDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var kullanıcılar = _context.AppUsers
                .Where(x => x.Rol.ToLower() != "admin")
                .ToList();
            return View(kullanıcılar);
        }

        public IActionResult Sil(int id)
        {
            var user = _context.AppUsers.Find(id);

            if (user != null && user.Rol.ToLower() != "admin")
            {
                var hoca = _context.OgretimGorevlileri.FirstOrDefault(x => x.AppUserId == id);
                if (hoca != null)
                {
                    var hocaninNotlari = _context.Notlar.Where(x => x.OgretimGorevlisiId == hoca.OgretimGorevlisiId);
                    _context.Notlar.RemoveRange(hocaninNotlari);

                    _context.OgretimGorevlileri.Remove(hoca);
                }
                var ogrenci = _context.Ogrenciler.FirstOrDefault(x => x.AppUserId == id);
                if (ogrenci != null)
                {
                    var ogrenciNotlari = _context.Notlar.Where(x => x.OgrenciId == ogrenci.OgrenciId);
                    _context.Notlar.RemoveRange(ogrenciNotlari);

                    _context.Ogrenciler.Remove(ogrenci);
                }

                _context.AppUsers.Remove(user);

                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}