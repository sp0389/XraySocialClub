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

        private async Task<Ticket> GetTicketByIdAsync(int id)
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

        public async Task ArchiveTicketAsync(int ticketId)
        {
            //TODO: Needs better error handing and perhaps a bool return type.

            var ticket = await GetTicketByIdAsync(ticketId) ?? throw new ApplicationException("No ticket was found with that ID.");

            ticket.SetInitialTicketState(ticket);
            ticket.ArchiveTicket();

            await _context.SaveChangesAsync();
        }

        public async Task <IEnumerable<Ticket>> GetTicketRecordsForMemberAsync(string memberId)
        {
            var tickets = await _context.TicketRecords
                .Include(tr => tr.Ticket)
                .Where(tr => tr.MemberId == memberId)
                .Select(t => t.Ticket)
                .ToListAsync() ?? throw new ApplicationException("No tickets were found for this member.");
                
            return tickets;
        }

        public async Task CreateTicketRecordForMemberAsync(TicketRecordViewModel m)
        {
            var ticket = await GetTicketByIdAsync(m.TicketId);
  
            foreach(var memberId in m.SelectedMemberId)
            {
                var checkRecordExists = await CheckIfTicketRecordExistsAsync(m.TicketId, memberId);

                if (!checkRecordExists)
                {
                    var member = await _organisationService.GetMemberByIdAsync(memberId);  
                    var ticketRecord = new TicketRecord(ticket, member);
                    await _context.TicketRecords.AddAsync(ticketRecord);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task<bool> CheckIfTicketRecordExistsAsync(int ticketId, string memberId)
        {
            var ticketRecord = await _context.TicketRecords
                .Where(tr => tr.TicketId == ticketId && tr.MemberId == memberId)
                .AnyAsync();
                
            return ticketRecord;
        }

        public async Task<decimal> TotalAmountSpentOnTicketsAsync()
        {
            var sum = await _context.Tickets.SumAsync(t => t.Price);
            return sum;
        }


        // TODO: This should correctly give a list of members who bought a specific ticket ID.
        public async Task <IEnumerable<Member>> GetMembersForTicketAsync(int ticketId)
        {
            var members = await _context.TicketRecords
                .Include(tr => tr.MemberId)
                .Where(tr => tr.TicketId == ticketId)
                .Select(tr => tr.Member)
                .ToListAsync();

            return members;
        }
    }
}