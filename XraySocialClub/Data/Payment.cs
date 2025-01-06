using System.ComponentModel.DataAnnotations;

namespace XraySocialClub.Data
{
    public enum PaymentType
    {
        Cash,
        [Display(Name = "Bank Transfer")]
        BankTransfer
    }

    public abstract class Payment
    {
        public int Id { get; set; }
        public Member Member { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public PaymentType Type { get; set; }
        public string Notes { get; set; } = default!;
        protected Payment() { }
        public Payment(Member member, decimal amount, DateTime datePaid, PaymentType type, string notes)
        {
            Member = member;
            Amount = amount;
            DatePaid = datePaid;
            Type = type;
            Notes = notes;
        }
    }

    public class LottoPayment : Payment
    {
        protected LottoPayment() { }
        public LottoPayment(Member member, decimal amount, DateTime datePaid, PaymentType type, string notes) : base(member, amount, datePaid, type, notes)
        {
            MemberId = member.Id;
        }
    }

    public class SocialPayment : Payment
    {
        protected SocialPayment() { }
        public SocialPayment(Member member, decimal amount, DateTime datePaid, PaymentType type, string notes) : base(member, amount, datePaid, type, notes)
        {
            MemberId = member.Id;
        }
    }
}