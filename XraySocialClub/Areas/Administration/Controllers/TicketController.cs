using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Areas.Administration.Models.Ticket;
using XraySocialClub.Data.Core;
using XraySocialClub.Services;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public class TicketController : AdministrationController
    {
        private readonly TicketService _ticketService;
        private readonly OrganisationService _organisationService;
        public TicketController(ApplicationDbContext context, ILogger<TicketController> logger, OrganisationService organisationService, TicketService ticketService)
            : base(context, logger)
        {
            _organisationService = organisationService;
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                return View(tickets);
            }
            
            catch(ApplicationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var m = new TicketViewModel()
            {
                DrawDate = DateTime.UtcNow,
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketViewModel m)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var ticket = await _ticketService.CreateNewTicketAsync(m);
                }

                catch(ApplicationException ex)
                {
                    TempData["Error"] = ex.Message;
                    return View(m);
                }
            }

            TempData["Success"] = "Ticket created successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateRecord()
        {
            var m = new TicketRecordViewModel()
            {
                Members = await _organisationService.GetLottoMembersAsync(),
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecord(TicketRecordViewModel m)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _ticketService.CreateTicketRecordForMemberAsync(m);
                }

                catch (ApplicationException ex)
                {
                    TempData["Error"] = ex.Message;
                    return View(m);
                }
            }
            return RedirectToAction("Index");
        }
    }
}