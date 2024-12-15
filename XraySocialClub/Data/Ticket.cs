namespace XraySocialClub.Data
{
    public enum TicketType
    {
        Powerball,
        Lotto,
    }

    public class Ticket
    {
        public int Id { get; set; }
        public string DrawNumber { get; set; } = default!;
        public DateTime DrawDate { get; set; }
        public string Notes { get; set; } = default!;
        public decimal Price { get; set; }
        public TicketType Type { get; set; }
        public ICollection<TicketRecord> TicketRecords { get; set; } = new List<TicketRecord>();
        private Ticket() { }
        public Ticket(string drawNumber, DateTime drawDate, string notes, TicketType ticketType)
        {
            DrawNumber = drawNumber;
            DrawDate = drawDate;
            Notes = notes;
            Type = ticketType;
        }
    }

    public class TicketRecord
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        private TicketRecord() { }
        public TicketRecord(Member member, Ticket ticket)
        {
            Member = member;
            Ticket =  ticket;
            MemberId = member.Id;
            TicketId = ticket.Id;
        }
    }
}