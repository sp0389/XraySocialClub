using Microsoft.EntityFrameworkCore;
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
    }
}