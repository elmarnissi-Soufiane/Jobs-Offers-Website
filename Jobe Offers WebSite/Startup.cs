using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {

        ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Create roles :: Pour Ajouter les roles . 
            CreateRoles();
            
        }

        // Create roles : il faut faire une relation entre la table RolesAsp . avec (DbContext) . 
        public void CreateRoles()
        {
            // RoleManager : il me donne la possibilite de gerer la class roles . 
            // RolesStore : il fait l'enregistrement sur la db . 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role;

            if (!roleManager.RoleExists("Admins"))
            {
                role = new IdentityRole();
                role.Name = "Admins";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.Email = "soufianeelmarnissi1997@live.fr";
                user.UserName = "soufiane";

                var check = userManager.Create(user, "@Ss123456@");

                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admins");
                }

            }

        }

    }
}
