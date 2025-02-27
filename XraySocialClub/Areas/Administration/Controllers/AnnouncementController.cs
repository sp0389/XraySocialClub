using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
using XraySocialClub.Data.Core;
using XraySocialClub.Services;

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

        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = 10;

                var announcements = await _announcementService.GetAllAnnouncements();
                var pagedAnnouncements = announcements.ToPagedList(pageNumber, pageSize);

                return View(pagedAnnouncements);
            }

            catch (ApplicationException ex)
            {
                TempData["Error"]  = ex.Message;
            }

            return View();
        }
    }
}

