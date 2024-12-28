using System.ComponentModel.DataAnnotations;
using XraySocialClub.Data;

namespace XraySocialClub.Areas.Administration.Models.Payment
{
    public class PaymentViewModel
    {
        public string? Id { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public DateTime? DatePaid { get; set; }
        [Required]
        public PaymentType? Type { get; set; }
        [MaxLength(100)]
        public string? Notes { get; set; } = default!;
    }
}