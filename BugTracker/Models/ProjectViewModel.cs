using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class ProjectViewModel
    {
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
    }

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
}