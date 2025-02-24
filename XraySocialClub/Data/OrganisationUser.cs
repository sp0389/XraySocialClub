using Microsoft.AspNetCore.Identity;

namespace XraySocialClub.Data
{
    public abstract class OrganisationUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Registered { get; set; }
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = default!;
        public ICollection<Role> UserRoles { get; set; } = new List<Role>();
        protected OrganisationUser() { }
        public OrganisationUser(int organisationId, string username, string firstName, string lastName, string email, Role? role)
        {
            OrganisationId = organisationId;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserRoles = [Role.Pending];
        }
    }

    public class Member : OrganisationUser
    {
        public ICollection<SocialPayment> SocialPayments { get; set; } = new List<SocialPayment>();
        public ICollection<LottoPayment> LottoPayments { get; set; } = new List<LottoPayment>();
        public ICollection<TicketRecord> TicketRecords { get; set; } = new List<TicketRecord>();
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

        //TODO: public to seed data for now -- change later
        public Member() { }
        
        public Member(int organisationId, string username, string firstName, string lastName, string email, Role? role) : base(organisationId, username, firstName, lastName, email, role) { }
        
        public SocialPayment NewSocialPayment(decimal amount, DateTime datePaid, PaymentType type, string notes)
        {
            var socialPayment = new SocialPayment(this, amount, datePaid, type, notes);
            SocialPayments.Add(socialPayment);
            return socialPayment;
        }

        public LottoPayment NewLottoPayment(decimal amount, DateTime datePaid, PaymentType type, string notes)
        {
            var lottoPayment = new LottoPayment(this, amount, datePaid, type, notes);
            LottoPayments.Add(lottoPayment);
            return lottoPayment;
        }
    }
}