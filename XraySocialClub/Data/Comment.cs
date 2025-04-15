namespace XraySocialClub.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; } = default!;
        private Comment() { }
        public Comment(string title, Member member, string message, DateTime createdAt, DateTime updatedAt, Announcement announcement)
        {
            Title = title;
            MemberId = member.Id;
            Message = message;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            AnnouncementId = announcement.Id;
        }
    }
}
