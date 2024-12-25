using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalonCruella.Areas.Identity.Data;
using SalonCruella.Data;
using SalonCruella.Models;

namespace SalonCruella.Controllers
{
    public class CalisanController : Controller
    {
        private readonly AppDbContext _context;

        public CalisanController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Calisan
        public async Task<IActionResult> Index()
        {
            var calisanlar = await _context.Calisanlar.Include(c => c.Salon).ToListAsync();
            return View(calisanlar);
        }

        // GET: Calisan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Geçersiz Çalışan ID.";
                return RedirectToAction(nameof(Index));
            }

            var calisan = await _context.Calisanlar
                .Include(c => c.Salon)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (calisan == null)
            {
                TempData["ErrorMessage"] = "Çalışan bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(calisan);
        }

        // GET: Calisan/Create
        public IActionResult Create()
        {
            ViewBag.Salonlar = new SelectList(_context.Salonlar, "Id", "Adi");
            return View();
        }

         // POST: Calisan/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
   
        

        // GET: Calisan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Geçersiz Çalışan ID.";
                return RedirectToAction(nameof(Index));
            }

            var calisan = await _context.Calisanlar.FindAsync(id);
            if (calisan == null)
            {
                TempData["ErrorMessage"] = "Çalışan bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Salonlar = new SelectList(_context.Salonlar, "Id", "Adi", calisan.SalonId);
            return View(calisan);
        }

        // POST: Calisan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,UzmanlikAlani,BaslangicSaati,BitisSaati,SalonId")] Calisan calisan)
        {
            if (id != calisan.Id)
            {
                TempData["ErrorMessage"] = "Çalışan ID eşleşmiyor.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Geçersiz giriş. Lütfen tüm alanları doğru doldurun.";
                ViewBag.Salonlar = new SelectList(_context.Salonlar, "Id", "Adi", calisan.SalonId);
                return View(calisan);
            }

            try
            {
                _context.Update(calisan);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Çalışan başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Çalışan güncellenirken bir hata oluştu.";
                Console.WriteLine($"Hata: {ex.Message}");
                ViewBag.Salonlar = new SelectList(_context.Salonlar, "Id", "Adi", calisan.SalonId);
                return View(calisan);
            }
        }

        // GET: Calisan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Geçersiz Çalışan ID.";
                return RedirectToAction(nameof(Index));
            }

            var calisan = await _context.Calisanlar
                .Include(c => c.Salon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calisan == null)
            {
                TempData["ErrorMessage"] = "Çalışan bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(calisan);
        }

        // POST: Calisan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var calisan = await _context.Calisanlar.FindAsync(id);
                if (calisan != null)
                {
                    _context.Calisanlar.Remove(calisan);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Çalışan başarıyla silindi.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Çalışan silinirken bir hata oluştu.";
                Console.WriteLine($"Hata: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
