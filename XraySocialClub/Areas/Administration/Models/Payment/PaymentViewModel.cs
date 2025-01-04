using System.ComponentModel.DataAnnotations;
using XraySocialClub.Data;

namespace XraySocialClub.Areas.Administration.Models.Payment
{
    public class PaymentViewModel
    {
        public string? MemberId { get; set; }
        [Required]
        [Range(0.05, 1000, ErrorMessage = "Price must be between 0.05 and 1000.00")]
        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? DatePaid { get; set; }
        [Required(ErrorMessage = "A payment type is required.")]
        public PaymentType? Type { get; set; }
        [Required(ErrorMessage = "A role type is required.")]
        public RolePaymentType? RolePaymentType { get; set; }
        [MaxLength(1000)]
        public string? Notes { get; set; } = default!;
    }
}