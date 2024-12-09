namespace XraySocialClub.Data
{
    public enum PaymentType
    {
        Cash,
        CreditCard,
        EFTPOS,
        BankTransfer
    }

    public abstract class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Notes { get; set; } = default!;
        protected Payment() { }
        public Payment(decimal amount, DateTime datePaid, PaymentType paymentType, string notes)
        {
            Amount = amount;
            DatePaid = datePaid;
            PaymentType = paymentType;
            Notes = notes;
        }
    }

    public class LottoPayment : Payment
    {
        protected LottoPayment() { }
        public string MemberId { get; set; } = default!;
        public LottoMember Member { get; set; } = default!;
        public LottoPayment(LottoMember member, decimal amount, DateTime datePaid, PaymentType paymentType, string notes) : base(amount, datePaid, paymentType, notes)
        {
            MemberId = member.Id;
        }
    }

    public class SocialPayment : Payment
    {
        protected SocialPayment() { }
        public string MemberId { get; set; } = default!;
        public SocialMember Member { get; set; } = default!;
        public SocialPayment(SocialMember member, decimal amount, DateTime datePaid, PaymentType paymentType, string notes) : base(amount, datePaid, paymentType, notes)
        {
            MemberId = member.Id;
        }
    }
}