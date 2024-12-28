namespace XraySocialClub.Areas.Administration.Models.Member;

public class EditViewModel : RegisterViewModel
{
    public string Id { get; set; } = default!;
    public IEnumerable<string> Roles { get; set; } = new List<string>();
    public string? RoleName { get; set; } = default!;
    public bool? RemoveRole { get; set; }
}