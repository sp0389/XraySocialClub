namespace XraySocialClub.Areas.Administration.Models.Ticket
{
    public class TicketRecordViewModel
    {
        public IEnumerable<Data.Member> Members { get; set; } = new List<Data.Member>();
        public IEnumerable<string> SelectedMembers { get; set; } = new List<string>();
        public int TicketId { get; set; }
    }
}