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
        private readonly CommentService _commentService;
        public AnnouncementController(ApplicationDbContext context, ILogger<AnnouncementController> logger, AnnouncementService announcementService,
            CommentService commentService)
            : base(context, logger)
        {
            _announcementService = announcementService;
            _commentService = commentService;
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
            var memberId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Not found.";

            if (ModelState.IsValid)
            {
                try
                {
                    var announcement = await _announcementService.CreateNewAnnouncementAsync(m, memberId);
                    return RedirectToAction("Index");
                }

                catch (ApplicationException ex)
                {
                    TempData["Error"] = ex.Message;
                    _logger.LogError(ex, "There was an error creating the announcement.");
                }
            }

            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult>GetAnnouncementCommentsById(int id)
        {
            var comments = await _commentService.GetCommentsByAnnouncementId(id);

            return PartialView("_AnnouncementCommentsPartial", comments);
        }
    }
}