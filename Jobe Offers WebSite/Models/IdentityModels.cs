using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Jobe_Offers_WebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser. Pour en savoir plus, consultez https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {

        public string UserType { get; set; }

        // Collection Jobs . tarbet bayna user et job . 
        public virtual ICollection<Job> Jobs { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
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

        public System.Data.Entity.DbSet<Jobe_Offers_WebSite.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Jobe_Offers_WebSite.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<Jobe_Offers_WebSite.Models.ApplyForJob> ApplyForJobs { get; set; }

        //public System.Data.Entity.DbSet<WebApplication1.Models.EditProfileViewModel> EditProfileViewModels { get; set; }

        //public System.Data.Entity.DbSet<WebApplication1.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}