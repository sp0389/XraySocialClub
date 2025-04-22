namespace XraySocialClub.Models
{

    //TODO: Add validation
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}