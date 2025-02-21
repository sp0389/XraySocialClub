using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
using XraySocialClub.Areas.Administration.Models.Purchase;
using XraySocialClub.Data.Core;
using XraySocialClub.Services;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public class PurchaseController : AdministrationController
    {
        private readonly PurchaseService _purchaseService;
        public PurchaseController(ApplicationDbContext context, ILogger<PurchaseController> logger,
            PurchaseService purchaseService) : base(context, logger)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = 10;

                var purchases = await _purchaseService.GetAllPurchaseRecordsAsync();
                var pagedPurchases = purchases.ToPagedList(pageNumber, pageSize);
                
                return View(pagedPurchases);
            }
            
            catch (ApplicationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var m = new PurchaseViewModel()
            {
                DatePurchased = DateTime.UtcNow,
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseViewModel m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var purchaseRecord = await _purchaseService.CreatePurchaseRecordAsync(m);
                }

                catch (ApplicationException ex)
                {
                    TempData["Error"] = ex.Message;
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
