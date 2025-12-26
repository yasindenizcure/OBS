using DenemeDers.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DenemeDers.Controllers
{
    [Authorize(Roles = "Ogrenci")] // Sadece öğrencilerin girmesine izin ver
    public class OgrenciPanelController : Controller
    {
        private readonly ContextDb _context;

        public OgrenciPanelController(ContextDb context)
        {
            _context = context;
        }

        public IActionResult Notlarim()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var notlarim = _context.Notlar
                .Include(x => x.Ders)
                .Include(x => x.OgretimGorevlisi)
                .Where(x => x.Ogrenci.AppUserId == userId)
                .ToList();

            return View(notlarim);
        }
    }
}

