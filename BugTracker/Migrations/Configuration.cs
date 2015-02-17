namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
                RoleManager.Create(new IdentityRole { Name = "Admin" });

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var me = context.Users.FirstOrDefault(u => u.Email == "setlifftg@gmail.com");

            if (me == null)
                UserManager.Create(new ApplicationUser
                {
                    UserName = "setlifftg@gmail.com",
                    Email = "setlifftg@gmail.com",
                    FirstName = "Tom",
                    LastName = "Setliff",
                }, "Abc123!");

            var user = UserManager.FindByEmail("setlifftg@gmail.com");

            if (!UserManager.IsInRole(user.Id, "Admin"))
                UserManager.AddToRole(user.Id, "Admin");
        }
    }
}
