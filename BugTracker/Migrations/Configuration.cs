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
        }

        //protected override void Seed(BugTracker.Models.TicketStatusContext context)
        //{            
        //    context.TicketStatus.AddOrUpdate(
        //        p => p.Name,
        //    new TicketStatuses
        //    {
        //        Name = "Open"
        //    },
        //      new TicketStatuses
        //      {
        //          Name = "Assigned"
        //      },
        //      new TicketStatuses
        //      {
        //          Name = "In Process"
        //      },
        //      new TicketStatuses
        //      {
        //          Name = "Testing"
        //      },
        //      new TicketStatuses
        //      {
        //          Name = "Submitter Approved"
        //      },
        //      new TicketStatuses
        //      {
        //          Name = "PM Approved"
        //      },
        //      new TicketStatuses
        //      {
        //          Name = "Resolved"
        //      }
        //      );
        //}

        //protected override void Seed(BugTracker.Models.TicketPriorityContext context)
        //{
        //    context.TicketPriority.AddOrUpdate(
        //        p => p.Name,
        //    new TicketPriorities
        //    {
        //        Name = "Low"
        //    },
        //      new TicketPriorities
        //      {
        //          Name = "Medium"
        //      },
        //      new TicketPriorities
        //      {
        //          Name = "High"
        //      },
        //      new TicketPriorities
        //      {
        //          Name = "Critical"
        //      }
        //      );
        //}

        //protected override void Seed(BugTracker.Models.TicketTypeContext context)
        //{
        //    context.TicketType.AddOrUpdate(
        //        p => p.Name,
        //    new TicketTypes
        //    {
        //        Name = "New Function"
        //    },
        //      new TicketTypes
        //      {
        //          Name = "Enhanced Function"
        //      },
        //      new TicketTypes
        //      {
        //          Name = "Broke Function"
        //      },
        //      new TicketTypes
        //      {
        //          Name = "Duplicate Request"
        //      }
        //      );
        //}
    }
}
