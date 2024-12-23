using Microsoft.AspNetCore.Mvc;
using SalonCruella.Data;
using SalonCruella.Models;

namespace SalonCruella.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Admin paneline ait bir ana sayfa
            return View();
        }

        public IActionResult SalonList()
        {
            var salonlar = _context.Salonlar.ToList();
            return View(salonlar);
        }

        public IActionResult CalisanList()
        {
            var calisanlar = _context.Calisanlar.ToList();
            return View(calisanlar);
        }

        public IActionResult RandevuList()
        {
            var randevular = _context.Randevular.ToList();
            return View(randevular);
        }

        // Diğer admin işlemleri burada tanımlanabilir
    }
}
