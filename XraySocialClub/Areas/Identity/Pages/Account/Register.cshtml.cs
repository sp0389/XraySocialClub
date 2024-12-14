// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;
using XraySocialClub.Services;

namespace XraySocialClub.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<OrganisationUser> _signInManager;
        private readonly OrganisationService _organisationService;

        public RegisterModel(SignInManager<OrganisationUser> signInManager, ApplicationDbContext context, OrganisationService organisationService)
        {
            _signInManager = signInManager;
            _context = context;
            _organisationService = organisationService;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            public int OrganisationId { get; set; } = 1;
            [Required]
            [DisplayName("First Name")]
            public string FirstName { get; set;}
            [Required]
            [DisplayName("Last Name")]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            public Role Role { get; set; } = Role.Pending;
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var member = await _organisationService.RegisterMemberAsync(Input.OrganisationId, Input.FirstName, Input.LastName, Input.Email, Input.Role, Input.Password);

                    await _signInManager.SignInAsync(await _context.Users.FirstAsync(user => user.Id == member.Id), false);
                    return LocalRedirect("/");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return Page();
        }

    }
}
