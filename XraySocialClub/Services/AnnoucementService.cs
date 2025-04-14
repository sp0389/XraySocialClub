using Microsoft.EntityFrameworkCore;
using XraySocialClub.Areas.Administration.Controllers;
using XraySocialClub.Areas.Administration.Models.Announcement;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Services
{
    public class AnnouncementService : BaseService
    {
        private readonly OrganisationService _organisationService;
        private readonly ImageService _imageService;

        public AnnouncementService(OrganisationService organisationService, ApplicationDbContext context, ImageService imageService)
            : base(context)
        {
            _organisationService = organisationService;
            _imageService = imageService;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync()
        {
            var announcements = await _context.Announcements
                .Include(a => a.Member)
                .OrderByDescending(a => a.Id).ToListAsync();

            return announcements;
        }

        public async Task<IEnumerable<Announcement>> GetLastThreeAnnouncementsAsync()
        {
            var announcements = await _context.Announcements
                .Include(a => a.Member)
                .OrderByDescending(a => a.Id)
                .Take(3)
                .ToListAsync();

            return announcements;
        }

        public async Task<bool> CreateNewAnnouncementAsync(AnnouncementViewModel m, string id)
        {
            var member = await _organisationService.GetMemberByIdAsync(id);
            
            if (m.Image != null)
            {
                var getImage = await _imageService.AddImageAsync(m.Image!);
                m.ImageUrl = getImage.Url.ToString();
            }
            
            var announcement = member.NewAnnouncement(m.Title!, member, m.Date!, m.ImageUrl!, m.Description!);

            await _context.AddAsync(announcement);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}