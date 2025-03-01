using Microsoft.EntityFrameworkCore;
using XraySocialClub.Areas.Administration.Models.Announcement;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;


namespace XraySocialClub.Services
{
    public class AnnouncementService
    {
        private readonly OrganisationService _organisationService;
        private readonly ApplicationDbContext _context;

        public AnnouncementService(OrganisationService organisationService, ApplicationDbContext context)
        {
            _organisationService = organisationService;
            _context = context;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncements()
        {
            var announcements = await _context.Announcements
                .OrderByDescending(a => a.Id).ToListAsync();

            return announcements;
        }

        public async Task<bool> CreateNewAnnouncementAsync(AnnouncementViewModel m, string id)
        {
            var member = await _organisationService.GetMemberByIdAsync(id);

            var announcement = member.NewAnnouncement(m.Title!, member, m.Date!.Value, m.Image!, m.Description!);

            await _context.AddAsync(announcement);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}