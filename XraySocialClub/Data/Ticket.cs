namespace XraySocialClub.Data
{
    public enum TicketType
    {
        Powerball,
        Lotto,
    }

    public enum TicketStatus
    {
        Active,
        Archived,
    }

    public class Ticket
    {
        private TicketState _state = default!;
        public int Id { get; set; }
        public string DrawNumber { get; set; } = default!;
        public DateTime DrawDate { get; set; }
        public string Notes { get; set; } = default!;
        public decimal Price { get; set; }
        public TicketType Type { get; set; }
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Active;
        public ICollection<TicketRecord> TicketRecords { get; set; } = new List<TicketRecord>();
        private Ticket() { }
        public Ticket(string drawNumber, DateTime drawDate, decimal price, string notes, TicketType ticketType)
        {
            DrawNumber = drawNumber;
            DrawDate = drawDate;
            Price = price;
            Notes = notes;
            Type = ticketType;
        }

        public void UpdateTicketState(TicketState state)
        {
            _state = state;
        }

        public void SetInitialTicketState(Ticket ticket)
        {
            switch(ticket.TicketStatus)
            {
                case TicketStatus.Active:
                    _state = new TicketState.ActiveState(this);
                    break;
                case TicketStatus.Archived:
                    _state = new TicketState.ArchiveState(this);
                    break;
            }
        }

        public void ArchiveTicket()
        {
            _state.ArchiveTicket(this);
        }

        //TODO: Right now we have no reason to activate a ticket since tickets are already activated when created, but added for future expansion for now.
        public void ActiveTicket()
        {
            _state.ActiveTicket(this);
        }
    }

    public class TicketRecord
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; } = default!;
        public string MemberId { get; set; } = default!;
        public Member Member { get; } = default!;
        private TicketRecord() { }
        public TicketRecord(Ticket ticket, Member member)
        {
            TicketId = ticket.Id;
            MemberId = member.Id;
        }
    }
}