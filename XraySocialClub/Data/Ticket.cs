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
        public TicketType TicketType { get; set; }
        private Ticket() { }
        public Ticket(string drawNumber, DateTime drawDate, string notes, TicketType ticketType)
        {
            DrawNumber = drawNumber;
            DrawDate = drawDate;
            Notes = notes;
            TicketType = ticketType;
        }
    }

    public class TicketRecord
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public LottoMember Member { get; set; } = default!;
        private TicketRecord() { }
        public TicketRecord(LottoMember member, Ticket ticket)
        {
            MemberId = member.Id;
            TicketId = ticket.Id;
        }
    }
}