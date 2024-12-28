using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonCruella.Data;

namespace SalonCruella.Controllers
{
    [Authorize(Roles = "Admin")] // Sadece Admin rolüne sahip kullanıcılar erişebilir
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
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
    }
}
