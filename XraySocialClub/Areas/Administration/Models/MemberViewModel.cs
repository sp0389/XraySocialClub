using XraySocialClub.Data;

namespace XraySocialClub.Areas.Administration.Models;

public class MemberViewModel
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public Role? Role { get; set; }
}