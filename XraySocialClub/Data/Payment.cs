namespace XraySocialClub.Data
{
    public enum PaymentType
    {
        Cash,
        BankTransfer
    }

    public abstract class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public PaymentType Type { get; set; }
        public string Notes { get; set; } = default!;
        protected Payment() { }
        public Payment(decimal amount, DateTime datePaid, PaymentType type, string notes)
        {
            Amount = amount;
            DatePaid = datePaid;
            Type = type;
            Notes = notes;
        }
    }

    public class LottoPayment : Payment
    {
        protected LottoPayment() { }
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public LottoPayment(Member member, decimal amount, DateTime datePaid, PaymentType type, string notes) : base(amount, datePaid, type, notes)
        {
            MemberId = member.Id;
        }
    }

    public class SocialPayment : Payment
    {
        protected SocialPayment() { }
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public SocialPayment(Member member, decimal amount, DateTime datePaid, PaymentType type, string notes) : base(amount, datePaid, type, notes)
        {
            MemberId = member.Id;
        }
    }
}