using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
using XraySocialClub.Areas.Administration.Models.Announcement;
using XraySocialClub.Data.Core;
using XraySocialClub.Services;
using System.Security.Claims;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public class AnnouncementController : AdministrationController
    {
        private readonly AnnouncementService _announcementService;
        public AnnouncementController(ApplicationDbContext context, ILogger<AnnouncementController> logger, AnnouncementService announcementService)
            : base(context, logger)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = 10;

                var announcements = await _announcementService.GetAllAnnouncementsAsync();
                var pagedAnnouncements = announcements.ToPagedList(pageNumber, pageSize);
                
                return View(pagedAnnouncements);
            }

            catch (ApplicationException ex)
            {
                TempData["Error"]  = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult>Create(AnnouncementViewModel m)
        {
            var memberId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                try
                {
                    var announcement = await _announcementService.CreateNewAnnouncementAsync(m, memberId!.Value);
                    return RedirectToAction("Index");
                }

                catch (ApplicationException ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            return View(m);
        }
    }
}

