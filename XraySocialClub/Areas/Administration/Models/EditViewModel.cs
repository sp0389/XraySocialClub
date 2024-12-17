using XraySocialClub.Data;

namespace XraySocialClub.Areas.Administration.Models;

public class EditViewModel
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; }    
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}
