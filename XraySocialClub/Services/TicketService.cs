using Microsoft.EntityFrameworkCore;
using XraySocialClub.Areas.Administration.Models.Ticket;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Services
{
    public class TicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly OrganisationService _organisationService;

        public TicketService(ApplicationDbContext context, OrganisationService organisationService)
        {
            _context = context;
            _organisationService = organisationService;
        }

        public async Task <IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket> CreateNewTicketAsync(TicketViewModel m)
        {
            var ticket = new Ticket(m.DrawNumber!, m.DrawDate!.Value, m.Price!.Value, m.Notes!, m.Type);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }
    }
}