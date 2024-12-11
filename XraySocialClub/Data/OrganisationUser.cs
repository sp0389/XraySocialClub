using Microsoft.AspNetCore.Identity;

namespace XraySocialClub.Data
{
    public class OrganisationUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Registered { get; set; }
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = default!;
        protected OrganisationUser() { }
        public OrganisationUser(int organisationId, string firstName, string lastName, string email)
        {
            OrganisationId = organisationId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }

    public class Member : OrganisationUser
    {
        public ICollection<SocialPayment> SocialPayments { get; set; } = new List<SocialPayment>();
        public ICollection<LottoPayment> LottoPayments { get; set; } = new List<LottoPayment>();
        public ICollection<TicketRecord> TicketRecords { get; set; } = new List<TicketRecord>();
        //TODO: public to seed data for now -- change later
        public Member() { }
        public Member(int organisationId, string firstName, string lastName, string email) : base(organisationId, firstName, lastName, email) { }
    }
}
