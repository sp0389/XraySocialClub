using System.ComponentModel.DataAnnotations;

namespace XraySocialClub.Areas.Administration.Models.Announcement
{
    public class AnnouncementViewModel()
    {
        [Required]
        [StringLength(50, ErrorMessage = "Title is too long.")]
        public string Title { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public IFormFile? Image { get; set; } = default!;
        public string ImageUrl { get; set; } = "No image specified.";
        [Required]
        public string Description { get; set; } = default!;
    }
}