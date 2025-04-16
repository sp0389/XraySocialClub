using Microsoft.EntityFrameworkCore;
using XraySocialClub.Areas.Administration.Controllers;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;
using XraySocialClub.Models;

namespace XraySocialClub.Services
{
    public class CommentService : BaseService
    {
        private readonly OrganisationService _organisationService;
        private readonly AnnouncementService _announcementService;

        public CommentService(ApplicationDbContext context, OrganisationService organisationService, AnnouncementService announcementService) 
            : base(context)
        {
            _organisationService = organisationService;
            _announcementService = announcementService;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            var comments = await _context.Comments.ToListAsync();

            return comments;
        }

        public async Task<Comment> CreateNewCommentAsync(CommentViewModel m, string memberId, int announcementId)
        {
            var member = await _organisationService.GetMemberByIdAsync(memberId);
            var announcement = await _announcementService.GetAnnouncementByIdAsync(announcementId);

            var comment = new Comment(m.Title, member, m.Message, m.CreatedAt, m.UpdatedAt, announcement);

            return comment;
        }
    }
}
