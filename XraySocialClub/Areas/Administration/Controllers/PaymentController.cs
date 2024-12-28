using Microsoft.AspNetCore.Mvc;
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

        // TODO: have two views for different types of payments for users (i.e. all/lotto/social)
        public async Task<IActionResult> Index(bool? isLotto)
        {
            switch (isLotto)
            {
                case true:
                    return View(await _paymentService.GetLottoPaymentsAsync());
                case false:
                    return View(await _paymentService.GetSocialPaymentsAsync());
                default:
                    return View(await _paymentService.GetAllPaymentsAsync());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}