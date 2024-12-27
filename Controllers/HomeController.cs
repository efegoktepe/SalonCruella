using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalonCruella.Models;

namespace SalonCruella.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Ana sayfa için bir görünüm döner
        return View();
    }

    public IActionResult Privacy()
    {
        // Gizlilik politikası sayfası için bir görünüm döner
        return View();
    }

    public IActionResult GoToAdminPanel()
    {
        // Admin paneline yönlendirme
        return RedirectToAction("Index", "Admin");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
