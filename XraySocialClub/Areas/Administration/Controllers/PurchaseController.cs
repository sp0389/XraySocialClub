using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Areas.Administration.Models.Purchase;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public class PurchaseController : AdministrationController
    {
        public PurchaseController(ApplicationDbContext context, ILogger<PurchaseController> logger)
            : base(context, logger) { }


        [HttpGet]
        public IActionResult Create()
        {
            var m = new PurchaseViewModel()
            {
                DatePurchased = DateTime.UtcNow,
            };

            return View(m);
        }


        //TODO: POST method for creating a new purchase record.
    }
}
