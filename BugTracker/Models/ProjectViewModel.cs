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