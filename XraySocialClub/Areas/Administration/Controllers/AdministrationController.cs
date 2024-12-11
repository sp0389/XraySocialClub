using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Controllers;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Areas.Administration.Controllers
{
    [Area("Administration"), Authorize(Roles = "Administrator")]
    public abstract class AdministrationController : BaseController
    {
        //TODO: temporarly set to 1 as we currently only have the one organisation. can be modified to support a multi tenant system.
        protected int OrganisationId => 1;
        public AdministrationController(ApplicationDbContext context, ILogger<AdministrationController> logger)
            : base(context, logger) { }
    }
}