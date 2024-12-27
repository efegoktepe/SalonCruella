using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonCruella.Data;
using SalonCruella.Models;

namespace SalonCruella.Controllers
{
    [Authorize(Roles = "User")] // Sadece User rolüne sahip kullanıcılar erişebilir
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Kullanıcının randevu alabileceği sayfa
        public IActionResult RandevuAl()
        {
            // Kullanıcının görebileceği salonların listesini getiriyoruz
            var salonlar = _context.Salonlar.ToList();
            return View(salonlar);
        }

        // Randevu oluşturma işlemi (POST metodu)
        [HttpPost]
        public IActionResult RandevuAl(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Randevular.Add(randevu);
                _context.SaveChanges();
                return RedirectToAction("RandevuOnay"); // Başarılı bir randevu alımı sonrası yönlendirme
            }
            return View(randevu); // Hatalı veri durumunda tekrar formu göster
        }

        // Randevu onay ekranı
        public IActionResult RandevuOnay()
        {
            return View(); // "Randevunuz başarıyla alındı" mesajı
        }
    }
}
