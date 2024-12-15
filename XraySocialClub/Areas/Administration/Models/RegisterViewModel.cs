using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XraySocialClub.Areas.Administration.Models;

public class RegisterViewModel
{
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Role { get; set; }
    public IEnumerable<SelectListItem> Roles { get; set; } = new List<SelectListItem>();  
}