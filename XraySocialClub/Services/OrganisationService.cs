using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            await _userManager.AddToRoleAsync(member, role!.Value.ToString());

            if (!result.Succeeded)
            {            
                throw new ApplicationException(result.Errors.First().Description);
            }
            return member;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            var members = await _context.Users.OfType<Member>()
                .ToListAsync();
            return members;
        }

        public async Task<IEnumerable<Member>> GetSocialMembersAsync()
        {
            var socialMembers = await _context.Users.OfType<Member>()
                .Where(sm => sm.UserRoles.Contains(Role.Social))
                .ToListAsync();
            return socialMembers;
        }

        public async Task<IEnumerable<Member>> GetLottoMembersAsync()
        {
            var lottoMembers = await _context.Users.OfType<Member>()
                .Where(lm => lm.UserRoles.Contains(Role.Lotto))
                .ToListAsync();
            return lottoMembers;
        }

        public async Task <IEnumerable<string>> GetUserRolesAsync(Member member)
        {
            if (member == null)
            {
                throw new ApplicationException("Member does not exist.");
            }

            var roles = await _userManager.GetRolesAsync(member);
            return roles;    
        }

        public async Task RemoveMemberFromRoleAsync(Member member, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(member, roleName);

            if (!result.Succeeded)
            {
                throw new ApplicationException(result.Errors.First().Description);
            }
        }

        public async Task AddMemberToRoleAsync(Member member, Role role)
        {
            member.UserRoles.Add(role);
            var result = await _userManager.AddToRoleAsync(member, role.ToString());

            if (!result.Succeeded)
            {
                throw new ApplicationException(result.Errors.First().Description);
            }
        }

        public async Task<Member> GetMemberByIdAsync(string id)
        {
            var member = await _userManager.FindByIdAsync(id) ?? throw new ApplicationException("No member was found with that ID.");
            return (Member)member;
        }

        public async Task UpdateMemberDetailsAsync(Member member)
        {
            var result = await _userManager.UpdateAsync(member);

            if (!result.Succeeded)
            {
                throw new ApplicationException(result.Errors.First().Description);
            }
        }
    }
}
