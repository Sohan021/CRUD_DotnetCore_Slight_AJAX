using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace crud_with_ajax.Models
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
