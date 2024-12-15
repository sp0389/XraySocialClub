using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XraySocialClub.Areas.Administration.Models;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Services
{
    public class OrganisationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OrganisationUser> _userManager;

        public OrganisationService(ApplicationDbContext context, UserManager<OrganisationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }

        public async Task<Member> RegisterMemberAsync(int organisationId, string firstName, string lastName, string email, Role? role, string password = "1")
        {
            var organisation = await _context.Organisations.FindAsync(organisationId) ?? throw new ApplicationException("No organisation could be found with that ID. Please check and try again.");

            var member = organisation.RegisterMember(organisation.Id, firstName, lastName, email, role);
            var result = await _userManager.CreateAsync(member, password);
            await _userManager.AddToRoleAsync(member, Role.Pending.ToString());

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return member;
            }
            
            throw new ApplicationException(result.Errors.First().Description);
        }

        public async Task <IEnumerable<MemberViewModel>> GetAllMembersAsync()
        {
            var members = await _context.Users.OfType<Member>()
                .ProjectToType<MemberViewModel>()
                .ToListAsync();
            return members;
        }

        public async Task <IEnumerable<MemberViewModel>> GetSocialMembersAsync()
        {
            var socialMembers = await _context.Users.OfType<Member>()
                .Where(sm => sm.Role == Role.Social)
                .ProjectToType<MemberViewModel>()
                .ToListAsync();
            return socialMembers;
        }

        public async Task <IEnumerable<MemberViewModel>> GetLottoMembersAsync()
        {
            var lottoMembers = await _context.Users.OfType<Member>()
                .Where(lm =>  lm.Role == Role.Lotto)
                .ProjectToType<MemberViewModel>()
                .ToListAsync();
            return lottoMembers;
        }
    }
}
