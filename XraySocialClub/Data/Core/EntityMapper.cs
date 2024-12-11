using Microsoft.EntityFrameworkCore;

namespace XraySocialClub.Data.Core
{
    public class EntityMapper
    {
        public EntityMapper(ModelBuilder mb)
        {
            mb.Entity<Organisation>(o =>
            {
                o.Property(o => o.Name).IsRequired();
            });

            mb.Entity<Payment>()
                .HasDiscriminator<string>("payment_type")
                .HasValue<SocialPayment>("payment_social")
                .HasValue<LottoPayment>("payment_lotto");

            mb.Entity<SocialPayment>(sp =>
            {
                sp.HasOne(sp => sp.Member)
                .WithMany(sm => sm.SocialPayments)
                .HasForeignKey(sp => sp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
                sp.Property(sp => sp.MemberId)
                .HasColumnName("SocialPayment_MemberId");
            });

            mb.Entity<LottoPayment>(lp =>
            {
                lp.HasOne(lp => lp.Member)
                .WithMany(lm => lm.LottoPayments)
                .HasForeignKey(lp => lp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
                lp.Property(sp => sp.MemberId)
                .HasColumnName("LottoPayment_MemberId");
            });

            mb.Entity<Member>()
                .HasDiscriminator<string>("member_type")
                .HasValue<SocialMember>("member_social")
                .HasValue<LottoMember>("member_lotto");

            mb.Entity<LottoMember>(lm =>
            {
                lm.HasMany(lm => lm.LottoPayments)
                  .WithOne(lp => lp.Member)
                  .HasForeignKey(lp => lp.MemberId)
                  .OnDelete(DeleteBehavior.Restrict);

                lm.HasMany(lm => lm.TicketRecords)
                  .WithOne(tr => tr.Member)
                  .HasForeignKey(tr => tr.MemberId)
                  .OnDelete(DeleteBehavior.Restrict); 
            });

            mb.Entity<SocialMember>()
                .HasMany(sm => sm.SocialPayments)
                .WithOne(sp => sp.Member)
                .HasForeignKey(sp => sp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

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

                tr.Property(tr => tr.MemberId)
                .HasColumnName("Lotto_MemberId");
            });
        }
    }
}