using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Controllers;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Areas.Administration.Controllers
{
    [Area("Administration"), Authorize(Roles = "Administrator")]
    public abstract class AdministrationController : BaseController
    {
        public AdministrationController(ApplicationDbContext context, ILogger<AdministrationController> logger)
            : base(context, logger) { }
    }
}