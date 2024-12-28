using System.ComponentModel.DataAnnotations;
using XraySocialClub.Data;

namespace XraySocialClub.Areas.Administration.Models.Member;

public class RegisterViewModel
{
    [Required(ErrorMessage = "A First Name Is Required."), MaxLength(25)]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "A Last Name Is Required."), MaxLength(25)]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "An Email Address Is Required."), MaxLength(50)]
    public string? Email { get; set; }
    [Required(ErrorMessage = "A Role Is Required.")]
    public Role? Role { get; set; }
}