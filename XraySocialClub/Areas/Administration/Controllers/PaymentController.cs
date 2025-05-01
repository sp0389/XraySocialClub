using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
using XraySocialClub.Areas.Administration.Models.Payment;
using XraySocialClub.Data.Core;
using XraySocialClub.Services;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public class PaymentController : AdministrationController
    {
        public readonly PaymentService _paymentService;
        public PaymentController(ApplicationDbContext context, ILogger<PaymentController> logger, PaymentService paymentService)
            : base(context, logger)
        {
            _paymentService = paymentService;
        }

        public async Task<IActionResult> Index(string id, int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = 10;

                var paymentRecord = await _paymentService.GetPaymentRecordForMemberAsync(id);
                var pagedPaymentReconds = paymentRecord.ToPagedList(pageNumber, pageSize);

                ViewBag.PaymentTotal = await _paymentService.GetSumOfMembersPaymentsAsync(id);
                return View(pagedPaymentReconds);
            }

            catch (ApplicationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var m = new PaymentViewModel()
            {
                MemberId = id,
                DatePaid = DateTime.UtcNow,
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string id, PaymentViewModel m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var paymentRecord = await _paymentService.CreatePaymentRecordForMemberAsync(id, m);
                }

                catch (ApplicationException ex)
                {
                    TempData["Error"] = ex.Message;
                    _logger.LogError(ex, "There was an error trying to create a payment record.");
                    return View(m);
                }
            }
            
            TempData["Success"] = "Payment Record created for member.";
            return RedirectToAction("Index", new {id});
        }
    }
}