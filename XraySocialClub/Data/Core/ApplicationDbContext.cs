using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace XraySocialClub.Data.Core
{
    public class ApplicationDbContext : IdentityDbContext<OrganisationUser>
    {
        public required DbSet<Organisation> Organisations { get; set; }
        public required DbSet<Payment> Payments { get; set; }
        public required DbSet<Ticket> Tickets { get; set; }
        public required DbSet<TicketRecord> TicketRecords { get; set; }
        public required DbSet<Purchase> Purchases { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            _ = new EntityMapper(builder);
            _ = new Seed(builder);
        }
    }
}