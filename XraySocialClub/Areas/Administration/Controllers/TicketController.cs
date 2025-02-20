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
        public async Task <IActionResult> Index(bool? isActive)
        {
            try
            {
                ViewBag.Total = await _ticketService.TotalAmountSpentOnTicketsAsync();

                switch(isActive)
                {
                    case true:
                        var activeTickets = await _ticketService.GetAllActiveTicketsAsync();
                        return View(activeTickets);
                    case false:
                        var archivedTickets = await _ticketService.GetArchivedTicketsAsync();
                        return View(archivedTickets);
                    default:
                        var tickets = await _ticketService.GetAllTicketsAsync();
                        return View(tickets);
                }
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

        [HttpPost]
        public async Task<IActionResult> ArchiveTicket(int id)
        {
            try
            {
                var ticket = await _ticketService.ArchiveTicketAsync(id);

                if (ticket == true)
                {
                    TempData["Success"] = "Ticket archived successfully.";
                }
                
                else
                {
                    TempData["Error"] = "Ticket could not be archived at this time.";
                }
            }

            catch (ApplicationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ActivateTicket(int id)
        {
            try
            {
                var ticket = await _ticketService.ActivateTicketAsync(id);

                if (ticket == true)
                {
                    TempData["Success"] = "Ticket activated successfully.";
                }

                else
                {
                    TempData["Error"] = "Ticket could not be activated at this time.";
                }
            }

            catch (ApplicationException ex)
            {
                //TODO: Add to view.
                TempData["Error"] = ex.Message;
            }

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
            TempData["Success"] = "Ticket record created successfully for member(s).";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> TicketRecord(string id)
        {
            try
            {
                var ticketRecords = await _ticketService.GetTicketRecordsForMemberAsync(id);
                return View(ticketRecords);
            }
            
            catch(ApplicationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewTicketMembers(int id)
        {
            try
            {
                var members = await _ticketService.GetMembersForTicketAsync(id);
                return PartialView("_TicketMembersPartial", members);
            }
            
            catch (ApplicationException ex)
            {
                TempData["Error"] = ex.Message;    
            }

            return View();
        }
    }
}