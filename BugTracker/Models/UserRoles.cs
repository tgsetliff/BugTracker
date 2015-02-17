using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class UserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Users User { get; set; }
        public virtual Roles Role { get; set; }
    }
}