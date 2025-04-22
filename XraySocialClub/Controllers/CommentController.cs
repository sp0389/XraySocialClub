using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;
using XraySocialClub.Models;
using XraySocialClub.Services;

namespace XraySocialClub.Controllers
{
    public class CommentController : BaseController
    {
        private readonly CommentService _commentService;
        private readonly UserManager<OrganisationUser> _userManager;
        public CommentController(ApplicationDbContext context, ILogger<CommentController> logger, CommentService commentService,
            UserManager<OrganisationUser> userManager)
            : base(context, logger)
        {
            _userManager = userManager;
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CommentFormPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel m, int announcementId)
        {
            if(ModelState.IsValid)
            {
                var memberId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Not found.";
                
                try
                {
                    var comment = await _commentService.CreateNewCommentAsync(m, memberId, announcementId);
                    var comments = await _commentService.GetCommentsAsync();
                    
                    //TODO: Could need adjusting later.
                    return PartialView("_AnnouncementCommentPartial", comments);
                }

                catch(ApplicationException ex)
                {
                    //TODO: Add this to the view.
                    TempData["Error"] = ex.Message;
                }
            }

            return PartialView("_CommentFormPartial");
        }
    }
}
