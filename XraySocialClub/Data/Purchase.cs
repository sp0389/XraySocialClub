namespace XraySocialClub.Data
{
    public class Purchase
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public decimal TotalPrice { get; set; }
        public string ReceiptNumber { get; set; } = default!;
        public DateTime DatePurchased { get; set; }

        public Purchase(string description, decimal totalPrice, string receiptNumber, DateTime datePurchased)
        {
            Description = description;
            TotalPrice = totalPrice;
            ReceiptNumber = receiptNumber;
            DatePurchased = datePurchased;
        }
    }
}
