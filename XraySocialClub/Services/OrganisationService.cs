using Microsoft.EntityFrameworkCore;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Services
{
    public class OrganisationService
    {
        private readonly ApplicationDbContext _context;

        public OrganisationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <IEnumerable<Member>> GetAllMembersAsync()
        {
            var members = await _context.Users.OfType<Member>().ToListAsync();
            return members;
        }

        public async Task <IEnumerable<Member>> GetSocialMembersAsync()
        {
            var socialMembers = await _context.Users.OfType<Member>()
                .Where(sm => sm.Role == Role.Social)
                .ToListAsync();
            return socialMembers;
        }

        public async Task <IEnumerable<Member>> GetLottoMembersAsync()
        {
            var lottoMembers = await _context.Users.OfType<Member>()
                .Where(lm =>  lm.Role == Role.Lotto)
                .ToListAsync();
            return lottoMembers;
        }
    }
}
