namespace XraySocialClub.Data
{
    public abstract class TicketState
    {
        public abstract void ActiveTicket(Ticket ticket);
        public abstract void ArchiveTicket(Ticket ticket);

        public class ActiveState : TicketState
        {
            public ActiveState(Ticket ticket)
            {
                ticket.TicketStatus = TicketStatus.Active;
            }

            public override void ActiveTicket(Ticket ticket)
            {
                throw new InvalidOperationException("You cannot active a ticket that is already active.");
            }

            public override void ArchiveTicket(Ticket ticket)
            {
                ticket.UpdateTicketState(new ArchiveState(ticket));
            }
        }

        public class ArchiveState : TicketState
        {
            public ArchiveState(Ticket ticket)
            {
                ticket.TicketStatus = TicketStatus.Archived;
            }

            public override void ActiveTicket(Ticket ticket)
            {
                throw new InvalidOperationException("You cannot activate an archived ticket.");
            }

            public override void ArchiveTicket(Ticket ticket)
            {
                throw new InvalidOperationException("You cannot archive a ticket that is already archived.");
            }
        }
    }
}