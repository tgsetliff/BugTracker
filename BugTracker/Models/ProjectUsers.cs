using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class ProjectUsers
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual Users User { get; set; }
    }
}