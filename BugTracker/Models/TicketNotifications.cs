using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketNotifications
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }

        public virtual Tickets Ticket { get; set; }
        public virtual Users User { get; set; }
    }
}