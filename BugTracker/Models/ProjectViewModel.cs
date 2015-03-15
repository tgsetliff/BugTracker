using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Models
{
    public class ProjectViewModel
    {
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }

        public int TotalTickets { get; set; }
        public int OpenTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public int OtherTickets { get; set; }

        public ProjectViewModel(Project project)
        {
            this.ProjectId = project.Id;
            this.ProjectName = project.Name;
            this.TotalTickets = project.Tickets.Count();
            this.OpenTickets = project.Tickets
                                .Where(t => t.TicketStatus.Name == "Open").Count();
            this.ResolvedTickets = project.Tickets
                                .Where(t => t.TicketStatus.Name == "Resolved").Count();
            this.OtherTickets = project.Tickets
                                .Where(t => t.TicketStatus.Name != "Open" && t.TicketStatus.Name != "Resolved").Count();
        }
    }

    [Bind(Exclude="AssignedUsers,UnAssignedUsers")]
    public class ProjectUserViewModel
    {
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }

        [Display(Name = "Assigned")]
        public System.Web.Mvc.MultiSelectList AssignedUsers { get; set; }
        public string[] UsersIn { get; set; }

        [Display(Name = "UnAssigned")]
        public System.Web.Mvc.MultiSelectList UnAssignedUsers { get; set; }
        public string[] UsersOut { get; set; }

    }

    [Bind(Exclude = "UnAssignedUsers")]
    public class CreateProjectUserViewModel
    {
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }

        [Display(Name = "UnAssigned")]
        public System.Web.Mvc.MultiSelectList UnAssignedUsers { get; set; }
        public string[] UsersIn { get; set; }

    }

}