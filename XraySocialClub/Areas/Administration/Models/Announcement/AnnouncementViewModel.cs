namespace XraySocialClub.Areas.Administration.Models.Announcement
{
    public class AnnouncementViewModel()
    {
        public int? Id { get; set; }
        public string? Title { get; set; } = default!;
        public Data.Member? Member { get; set; } = default!;
        public DateTime? Date { get; set; }
        public string? Image { get; set;} = default!;
        public string? Description { get; set; } = default!;
    }
}