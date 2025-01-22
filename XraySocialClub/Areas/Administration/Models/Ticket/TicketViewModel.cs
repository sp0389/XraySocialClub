using System.ComponentModel.DataAnnotations;
using XraySocialClub.Data;

namespace XraySocialClub.Areas.Administration.Models.Ticket
{
    public class TicketViewModel
    {
        [Required(ErrorMessage = "Draw number is required")]
        [Display(Name = "Draw number")]
        public string? DrawNumber { get; set; }
        [Required(ErrorMessage = "Draw date is required")]
        [Display(Name = "Draw date")]
        public DateTime? DrawDate { get; set; }
        public string? Notes { get; set; }
        [Required, Range(0.05, 1000, ErrorMessage = "Price must be between 0.05 and 1000")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "A ticket type is required")]
        public TicketType Type { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}