using System.ComponentModel.DataAnnotations;

namespace XraySocialClub.Areas.Administration.Models.Purchase
{
    public class PurchaseViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "A description of the purchase is required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The total price of the purchase is required.")]
        [Range(0.01, 10000, ErrorMessage = "The total price must be between 0.01 and 10000.")]
        public decimal? TotalPrice { get; set; }
        public string? ReceiptNumber { get; set; }
        [Required(ErrorMessage = "The date of the purchase is required.")]
        public DateTime? DatePurchased { get; set; }
    }
}