using Microsoft.AspNetCore.Mvc;

namespace DenemeDers.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
