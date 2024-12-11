using XraySocialClub.Data.Core;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public class MemberController : AdministrationController
    {
        public MemberController(ApplicationDbContext context, ILogger<MemberController> logger)
            : base(context, logger) { }
    }
}
