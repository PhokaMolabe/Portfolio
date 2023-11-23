namespace SLD521SA.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SLD521SA.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SLD521SA.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "SLD521SA.Models.ApplicationDbContext";
        }

        protected override void Seed(SLD521SA.Models.ApplicationDbContext context)
        {
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);

            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            IdentityRole adminRole = new IdentityRole { Name = "Admin" };
            IdentityRole authorRole = new IdentityRole { Name = "Author" };

            if (!roleManager.RoleExists(adminRole.Name))
                roleManager.Create(adminRole);

            if (!roleManager.RoleExists(authorRole.Name))
                roleManager.Create(authorRole);

            // Initialize Default Admin
            ApplicationUser adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@ctu.co.za"
            };

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            var results = userManager.Create(adminUser, "!Admin1");

            if (results.Succeeded)
                userManager.AddToRoles(adminUser.Id, adminRole.Name);

            // Initialize tables
            base.Seed(context);
        }
    }
}
