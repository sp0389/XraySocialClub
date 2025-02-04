namespace XraySocialClub.Data
{
    public class Organisation
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Organisation() { }
        public Member RegisterMember(int organisationId, string firstName, string lastName, string email, Role? role)
        {
            var member = new Member(organisationId, email, firstName, lastName, email, role)
            {
                Registered = DateTime.UtcNow
            };
            
            return member;
        }
    }
}