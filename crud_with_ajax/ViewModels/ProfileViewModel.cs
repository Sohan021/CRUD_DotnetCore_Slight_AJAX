using crud_with_ajax.Utilities;
using System.ComponentModel.DataAnnotations;

namespace crud_with_ajax.ViewModels
{
    public class ProfileViewModel
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [ValidEmailDomain(allowedDomain: "gmail.com",
        ErrorMessage = "Email domain must be gmail.com")]
        public string Email { get; set; }

        [Required]
        public string ContactNo { get; set; }


    }
}
