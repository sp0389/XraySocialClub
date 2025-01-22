namespace XraySocialClub.Areas.Administration.Models.Member
{
    public class EditViewModel : RegisterViewModel
    {
        public string? Id { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public string? RoleName { get; set; }
        public bool? RemoveRole { get; set; }
    }
}