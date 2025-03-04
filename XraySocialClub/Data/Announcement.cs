namespace XraySocialClub.Data
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public DateTime Date { get; set; }
        // for image service (cloudinary?)
        public string ImageUrl { get; set;} = default!;
        public string Description { get; set; } = default!;

        private Announcement() {}

        public Announcement (string title, Member member, DateTime date, string imageUrl, string description)
        {
            Title = title;
            MemberId = member.Id;
            Date = date;
            ImageUrl = imageUrl;
            Description = description;
        }
    }
}