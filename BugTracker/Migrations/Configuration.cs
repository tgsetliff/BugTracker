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

            if (!context.Roles.Any(r => r.Name == "PM"))
                RoleManager.Create(new IdentityRole { Name = "PM" });

            if (!context.Roles.Any(r => r.Name == "Developer"))
                RoleManager.Create(new IdentityRole { Name = "Developer" });

            if (!context.Roles.Any(r => r.Name == "Submitter"))
                RoleManager.Create(new IdentityRole { Name = "Submitter" });


            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var me = context.Users.FirstOrDefault(u => u.Email == "setlifftg@gmail.com");

            if (me == null)
                UserManager.Create(new ApplicationUser
                {
                    UserName = "setlifftg@gmail.com",
                    Email = "setlifftg@gmail.com",
                    FirstName = "Tom",
                    LastName = "Setliff",
                }, "t@ll3Dega");

            var user = UserManager.FindByEmail("setlifftg@gmail.com");

            if (!UserManager.IsInRole(user.Id, "Admin"))
                UserManager.AddToRole(user.Id, "Admin");

            context.TicketStatuses.AddOrUpdate(
                p => p.Name,
            new TicketStatus
            {
                Name = "Open"
            },
              new TicketStatus
              {
                  Name = "Assigned"
              },
              new TicketStatus
              {
                  Name = "In Process"
              },
              new TicketStatus
              {
                  Name = "Resolved"
              }
              );

             context.TicketPriorities.AddOrUpdate(
                p => p.Name,
            new TicketPriority
            {
                Name = "Low"
            },
              new TicketPriority
              {
                  Name = "Medium"
              },
              new TicketPriority
              {
                  Name = "High"
              },
              new TicketPriority
              {
                  Name = "Critical"
              }
              );

            context.TicketTypes.AddOrUpdate(
                p => p.Name,
            new TicketType
            {
                Name = "New Function"
            },
              new TicketType
              {
                  Name = "Enhanced Function"
              },
              new TicketType
              {
                  Name = "Broke Function"
              },
              new TicketType
              {
                  Name = "Duplicate Request"
              }
              );
        }
    }
}
