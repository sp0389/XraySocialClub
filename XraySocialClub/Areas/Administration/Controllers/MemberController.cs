using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Areas.Administration.Models;
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
        public async Task<IActionResult> Index(bool? isLotto)
        {
            switch (isLotto)
            {
                case true:
                    var lottoMembers = await _organisationService.GetLottoMembersAsync();
                    return View(lottoMembers);
                case false:
                    var socialMembers = await _organisationService.GetSocialMembersAsync();
                    return View(socialMembers);
                default:
                    var members = await _organisationService.GetAllMembersAsync();
                    return View(members);
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
                    var member = await _organisationService.RegisterMemberAsync(1, m.FirstName!, m.LastName!, m.Email!, m.Role);
                    return RedirectToAction("Index");
                }
                catch (ApplicationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(m);
        }
    }
}