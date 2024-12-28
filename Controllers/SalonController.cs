using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonCruella.Data;
using SalonCruella.Models;

namespace SalonCruella.Controllers
{
    public class SalonController : Controller
    {
        private readonly AppDbContext _context;

        public SalonController(AppDbContext context)
        {
           this._context = context;
        }

        // GET: Salon
        public async Task<IActionResult> Index()
        {
            var salonlar = await _context.Salonlar.ToListAsync();
            return View(salonlar);
        }

        // GET: Salon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Geçersiz salon ID.");
            }

            var salon = await _context.Salonlar.FindAsync(id);
            if (salon == null)
            {
                return NotFound("Salon bulunamadı.");
            }

            return View(salon);
        }

        // GET: Salon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Adi,Adres")] Salon salon)
        {
            if (!ModelState.IsValid)
            {
                return View(salon);
            }

            try
            {
                _context.Add(salon);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Salon başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Salon eklenirken bir hata oluştu.");
                return View(salon);
            }
        }

        // GET: Salon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Geçersiz salon ID.");
            }

            var salon = await _context.Salonlar.FindAsync(id);
            if (salon == null)
            {
                return NotFound("Salon bulunamadı.");
            }

            return View(salon);
        }

        // POST: Salon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,Adres")] Salon salon)
        {
            if (id != salon.Id)
            {
                return BadRequest("Salon ID eşleşmiyor.");
            }

            if (!ModelState.IsValid)
            {
                return View(salon);
            }

            try
            {
                _context.Update(salon);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Salon başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalonExists(salon.Id))
                {
                    return NotFound("Salon bulunamadı.");
                }
                throw;
            }
        }

        // GET: Salon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Geçersiz salon ID.");
            }

            var salon = await _context.Salonlar.FindAsync(id);
            if (salon == null)
            {
                return NotFound("Salon bulunamadı.");
            }

            return View(salon);
        }

        // POST: Salon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var salon = await _context.Salonlar.FindAsync(id);
                if (salon != null)
                {
                    _context.Salonlar.Remove(salon);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Salon başarıyla silindi.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Salon silinirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool SalonExists(int id)
        {
            return _context.Salonlar.Any(e => e.Id == id);
        }
    }
}
