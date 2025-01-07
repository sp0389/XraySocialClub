using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Areas.Administration.Models.Ticket;
using XraySocialClub.Data.Core;
using XraySocialClub.Services;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public class TicketController : AdministrationController
    {
        private readonly TicketService _ticketService;
        public TicketController(ApplicationDbContext context, ILogger<TicketController> logger, TicketService ticketService)
            : base(context, logger)
        {
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
    }
}