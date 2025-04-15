using Microsoft.EntityFrameworkCore;

namespace XraySocialClub.Data.Core
{
    public class EntityMapper
    {
        public EntityMapper(ModelBuilder mb)
        {
            mb.Entity<Organisation>(o =>
            {
                o.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50);
            });

            mb.Entity<Payment>()
                .HasDiscriminator<string>("Payment_Type")
                .HasValue<SocialPayment>("Payment_Social")
                .HasValue<LottoPayment>("Payment_Lotto");

            mb.Entity<SocialPayment>(sp =>
            {
                sp.HasOne(sp => sp.Member)
                .WithMany(sm => sm.SocialPayments)
                .HasForeignKey(sp => sp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
                sp.Property(sp => sp.MemberId)
                .HasColumnName("MemberId");
            });

            mb.Entity<LottoPayment>(lp =>
            {
                lp.HasOne(lp => lp.Member)
                .WithMany(lm => lm.LottoPayments)
                .HasForeignKey(lp => lp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
                lp.Property(sp => sp.MemberId)
                .HasColumnName("MemberId");
            });

            mb.Entity<Member>(m =>
            {
                m.HasMany(m => m.LottoPayments)
                .WithOne(lp => lp.Member)
                .HasForeignKey(lp => lp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

                m.HasMany(m => m.TicketRecords)
                .WithOne(tr => tr.Member)
                .HasForeignKey(tr => tr.MemberId)
                .OnDelete(DeleteBehavior.Restrict); 

                m.HasMany(m => m.SocialPayments)
                .WithOne(sp => sp.Member)
                .HasForeignKey(sp => sp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

                m.HasMany(m => m.Announcements)
                .WithOne(a => a.Member)
                .HasForeignKey(a => a.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

                m.HasMany(m => m.Comments)
                .WithOne(c => c.Member)
                .HasForeignKey(c => c.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<TicketRecord>(tr => 
            {
                tr.HasOne(tr => tr.Member)
                .WithMany(m => m.TicketRecords)
                .HasForeignKey(tr => tr.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

                tr.HasOne(tr => tr.Ticket)
                .WithMany(t => t.TicketRecords)
                .HasForeignKey(tr => tr.TicketId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<Announcement>(a => {
                a.HasOne(a => a.Member)
                .WithMany(m => m.Announcements)
                .HasForeignKey(a => a.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

                a.HasMany(a => a.Comments)
                .WithOne(c => c.Announcement)
                .HasForeignKey(c => c.AnnouncementId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<Comment>(c =>
            {
                c.HasOne(c => c.Member)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
                
                c.HasOne(c => c.Announcement)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AnnouncementId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}