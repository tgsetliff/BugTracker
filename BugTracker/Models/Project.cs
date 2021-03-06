﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Project
    {        
        public Project()
        {
            Tickets = new HashSet<Ticket>();
            AssignedUsers = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> AssignedUsers { get; set; }
    }
}