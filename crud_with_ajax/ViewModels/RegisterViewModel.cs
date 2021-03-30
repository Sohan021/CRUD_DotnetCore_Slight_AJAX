using crud_with_ajax.Utilities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace crud_with_ajax.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [ValidEmailDomain(allowedDomain: "gmail.com",
        ErrorMessage = "Email domain must be gmail.com")]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IFormFile Photo { get; set; }

        public string RoleId { get; set; }

        public string CityId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
