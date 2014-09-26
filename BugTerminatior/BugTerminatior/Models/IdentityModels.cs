using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTerminatior.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// ///////////////
        /// </summary>

        private UserManager<ApplicationUser> usermanager = 
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));


        public bool IsInRole(string rolename)
        {
            var result = usermanager.IsInRole(this.Id, rolename);
            return result;
        }

        public bool AddUserToRole(string rolename)
        {
            var result = usermanager.AddToRole(this.Id, rolename);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string rolename)
        {
            var result = usermanager.RemoveFromRole(this.Id, rolename);
            return result.Succeeded;
        }
        /////////////////////
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}