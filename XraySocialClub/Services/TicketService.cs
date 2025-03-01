using Microsoft.EntityFrameworkCore;
using XraySocialClub.Areas.Administration.Controllers;
using XraySocialClub.Areas.Administration.Models.Ticket;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Services
{
    public class TicketService : BaseService
    {
        private readonly OrganisationService _organisationService;

        public TicketService(ApplicationDbContext context, OrganisationService organisationService)
            : base(context)
        {
            _organisationService = organisationService;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            var tickets = await _context.Tickets.ToListAsync();

            return tickets;
        }

        public async Task<IEnumerable<Ticket>> GetAllActiveTicketsAsync()
        {
            var tickets = await _context.Tickets.Where(t => t.TicketStatus == TicketStatus.Active).ToListAsync();

            return tickets;
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

        public async Task<bool> ArchiveTicketAsync(int id)
        {
            var ticket = await GetTicketByIdAsync(id);

            if (ticket != null)
            {
                ticket.SetInitialTicketState(ticket);
                ticket.ArchiveTicket();
            }
            
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Ticket>> GetTicketRecordsForMemberAsync(string memberId)
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

            foreach (var memberId in m.SelectedMemberId)
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

        public async Task<IEnumerable<Member>> GetMembersForTicketAsync(int ticketId)
        {
            var members = await _context.TicketRecords
                .Include(tr => tr.Member)
                .Where(tr => tr.TicketId == ticketId)
                .Select(tr => tr.Member)
                .ToListAsync();

            return members;
        }

        //TODO: Need to add rest of functionality for this service. (View/Controller etc)
        public async Task<IEnumerable<Ticket>> GetArchivedTicketsAsync()
        {
            var tickets = await _context.Tickets.Where(t => t.TicketStatus == TicketStatus.Archived).ToListAsync();

            return tickets;
        }

        public async Task<bool> ActivateTicketAsync(int ticketId)
        {
            var ticket = await GetTicketByIdAsync(ticketId);

            if (ticket != null)
            {
                ticket.SetInitialTicketState(ticket);
                ticket.ActivateTicket();
            }
            
            return await _context.SaveChangesAsync() > 0;
        }
    }
}