using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace crud_with_ajax.Models
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Photo { get; set; }

        public virtual Role Role { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public ICollection<UserRole> Roles { get; set; }
    }
}
