using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class UserLogins
    {
        public int LoginProvider { get; set; }
        public int ProviderKey { get; set; }
        public int UserId { get; set; }
    }
}