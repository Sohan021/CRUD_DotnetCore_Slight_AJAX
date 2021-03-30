using crud_with_ajax.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace crud_with_ajax.Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, string,
                                IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
                                IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        //public virtual DbSet<User> AppUsers { get; set; }
        //public virtual DbSet<Role> AppRoles { get; set; }


    }
}
