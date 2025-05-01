using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
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
        public async Task <IActionResult> Index(bool? isActive, int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = 10;

                ViewBag.Total = await _ticketService.TotalAmountSpentOnTicketsAsync();

                switch(isActive)
                {
                    case true:
                        var activeTickets = await _ticketService.GetAllActiveTicketsAsync();
                        var pagedActiveTickets = activeTickets.ToPagedList(pageNumber, pageSize);
                        return View(pagedActiveTickets);
                    case false:
                        var archivedTickets = await _ticketService.GetArchivedTicketsAsync();
                        var pagedArchivedTickets = archivedTickets.ToPagedList(pageNumber, pageSize);
                        return View(pagedArchivedTickets);
                    default:
                        var tickets = await _ticketService.GetAllTicketsAsync();
                        var pagedTickets = tickets.ToPagedList(pageNumber, pageSize);
                        return View(pagedTickets);
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
                // Add to view
                TempData["Error"] = ex.Message;
            }

            return Json(new { redirectToUrl = Url.Action("Index")});
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
                // Add to view
                TempData["Error"] = ex.Message;
                _logger.LogError(ex, "There was an error activating the ticket.");
            }

            return Json(new { redirectToUrl = Url.Action("Index")});
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
                    _logger.LogError(ex, "There was an error creating the ticket record.");
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