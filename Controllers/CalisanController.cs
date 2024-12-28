using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var calisanlar = await _context.Calisanlar.ToListAsync();
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
            return View();
        }

        // POST: Calisan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Adi,UzmanlikAlani,BaslangicSaati,BitisSaati")] Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Calisanlar.Add(calisan);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Çalışan başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Çalışan eklenirken bir hata oluştu.";
                    Console.WriteLine($"Hata: {ex.Message}");
                    return View(calisan);
                }
            }

            return View(calisan);
        }

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

            return View(calisan);
        }

        // POST: Calisan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,UzmanlikAlani,BaslangicSaati,BitisSaati")] Calisan calisan)
        {
            if (id != calisan.Id)
            {
                TempData["ErrorMessage"] = "Çalışan ID eşleşmiyor.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calisan);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Çalışan başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["ErrorMessage"] = "Çalışan güncellenirken bir hata oluştu.";
                    Console.WriteLine("Veritabanı güncelleme hatası.");
                }
            }

            return View(calisan);
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
