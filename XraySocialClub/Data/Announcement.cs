namespace XraySocialClub.Data
{
    public class Announcement
    {
        public int Id { get; set; }
        public int AnnouncementId { get; set;}
        public string Title { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public DateTime Date { get; set; }
        public string Image { get; set;} = default!;
        public string Description { get; set; } = default!;

        private Announcement() {}

        public Announcement (string title, Member member, DateTime date, string image, string description)
        {
            Title = title;
            Member = member;
            Date = date;
            Image = image;
            Description = description;
        }
    }
}