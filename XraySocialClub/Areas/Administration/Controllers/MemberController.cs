using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
using XraySocialClub.Areas.Administration.Models.Member;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;
using XraySocialClub.Services;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public class MemberController : AdministrationController
    {
        private readonly OrganisationService _organisationService;
        public MemberController(ApplicationDbContext context, ILogger<MemberController> logger, OrganisationService organisationService)
            : base(context, logger)
        {
            _organisationService = organisationService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(bool? isLotto, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            
            switch (isLotto)
            {
                case true:
                    var lottoMembers = await _organisationService.GetLottoMembersAsync();
                    var pagedLottoMembers = lottoMembers.ToPagedList(pageNumber, pageSize);
                    return View(pagedLottoMembers);
                case false:
                    var socialMembers = await _organisationService.GetSocialMembersAsync();
                    var pagedSocialMembers = socialMembers.ToPagedList(pageNumber, pageSize);
                    return View(pagedSocialMembers);
                default:
                    var members = await _organisationService.GetAllMembersAsync();
                    var pagedMembers = members.ToPagedList(pageNumber, pageSize);
                    return View(pagedMembers);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel m)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _organisationService.RegisterMemberAsync(1, m.FirstName!, m.LastName!, m.Email!, m.Role);
                    return RedirectToAction("Index");
                }

                catch (ApplicationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var member = await _organisationService.GetMemberByIdAsync(id);

                var m = new EditViewModel()
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Email = member.Email ?? "Not available",
                    Roles = await _organisationService.GetUserRolesAsync(member)
                };

                return View(m);
            }

            catch(ApplicationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel m)
        {
            var member = new Member();

            try
            {
                member = await _organisationService.GetMemberByIdAsync(m.Id!);
                
                if (m.RoleName != null && m.RemoveRole == true)
                {
                    switch(m.RoleName)
                    {
                        case "Social":
                            member.UserRoles.Remove(Role.Social);
                            break;
                        case "Lotto":
                            member.UserRoles.Remove(Role.Lotto);
                            break;
                        case "Pending":
                            member.UserRoles.Remove(Role.Pending);
                            break;
                        case "Administrator":
                            member.UserRoles.Remove(Role.Administrator);
                            break;
                    }
                    
                    await _organisationService.RemoveMemberFromRoleAsync(member, m.RoleName);
                }
            }

            catch(ApplicationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            if (ModelState.IsValid)
            {
                member.FirstName = m.FirstName!;
                member.LastName = m.LastName!;
                member.Email = m.Email;
                
                try
                {
                    await _organisationService.UpdateMemberDetailsAsync(member);
                    await _organisationService.AddMemberToRoleAsync(member, m.Role!.Value);  
                }

                catch (ApplicationException ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            TempData["Success"] = "Member Details Updated Sucessfully.";
            
            return RedirectToAction("Edit");
        }
    }
}