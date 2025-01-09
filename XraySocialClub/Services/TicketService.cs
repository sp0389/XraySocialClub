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

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id) ?? throw new ApplicationException("No ticket was found with that ID.");

            return ticket;
        }

        public async Task<Ticket> CreateNewTicketAsync(TicketViewModel m)
        {
            var ticket = new Ticket(m.DrawNumber!, m.DrawDate!.Value, m.Price!.Value, m.Notes!, m.Type);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        public async Task <IEnumerable<Ticket>> GetTicketRecordsForMemberAsync(string memberId)
        {
            var tickets = await _context.TicketRecords
                .Include(tr => tr.Ticket)
                .Where(tr => tr.MemberId == memberId)
                .Select(t => t.Ticket)
                .ToListAsync();
                
            return tickets;
        }

        public async Task CreateTicketRecordForMemberAsync(TicketRecordViewModel m)
        {
            var ticket = await GetTicketByIdAsync(m.TicketId);
  
            foreach(var memberId in m.SelectedMemberId)
            {
                var member = await _organisationService.GetMemberByIdAsync(memberId);

                var ticketRecord = new TicketRecord(ticket, member);
                await _context.TicketRecords.AddAsync(ticketRecord);
            }
            
            await _context.SaveChangesAsync();
        }
    }
}