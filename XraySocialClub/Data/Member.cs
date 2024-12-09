using Microsoft.AspNetCore.Identity;

namespace XraySocialClub.Data
{
    public abstract class Member : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Registered { get; set; }
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = default!;
        protected Member() { }
        public Member(int organisationId, string firstName, string lastName, string email)
        {
            OrganisationId = organisationId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }

    public class SocialMember : Member
    {
        public ICollection<SocialPayment> SocialPayments { get; set; } = new List<SocialPayment>();
        protected SocialMember() { }
        public SocialMember(int organisationId, string firstName, string lastName, string email) : base(organisationId, firstName, lastName, email) { }
    }

    public class LottoMember : Member
    {
        public ICollection<LottoPayment> LottoPayments { get; set; } = new List<LottoPayment>();
        public ICollection<TicketRecord> TicketRecords { get; set; } = new List<TicketRecord>();
        protected LottoMember() { }
        public LottoMember(int organisationId, string firstName, string lastName, string email) : base(organisationId, firstName, lastName, email) { }
    }
}
