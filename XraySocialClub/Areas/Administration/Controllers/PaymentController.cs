using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(string id)
        {
            try
            {
                var paymentRecord = await _paymentService.GetPaymentRecordForMemberAsync(id);
                return View(paymentRecord);
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
                Id = id,
                DatePaid = DateTime.UtcNow,
            };

            return View(m);
        }
    }
}