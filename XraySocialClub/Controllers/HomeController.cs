using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Models;
using XraySocialClub.Services;

namespace XraySocialClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AnnouncementService _announcementService;

        public HomeController(ILogger<HomeController> logger, AnnouncementService announcementService)
        {
            _logger = logger;
            _announcementService = announcementService;
        }

        public async Task<IActionResult> Index()
        {
            var announcements = await _announcementService.GetLastThreeAnnouncementsAsync();
            return View(announcements);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
