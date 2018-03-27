using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System;
using System.Data.Common;

namespace LibraryLite.UI.Web.MVC.Models
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
       
        public static ApplicationUser Create()
        {
            return new ApplicationUser();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("LibraryLiteDb.SqlServer")
        {
        }
        public IDbSet<IdentityUserClaim> UserClaim { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                .ToTable("User");

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("User");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogin");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRole");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");
        }
        
    }
    public static class UserClaim
    {
        public static IList<string> UserClaims { get; set; }

        static UserClaim()
        {
            UserClaims = new List<string>();
           
            UserClaims.Add("User");
            UserClaims.Add("Staff");
            UserClaims.Add("Manager");
            UserClaims.Add("Admin");
        }
    }
}