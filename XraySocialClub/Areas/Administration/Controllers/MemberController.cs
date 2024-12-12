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
    }
}
