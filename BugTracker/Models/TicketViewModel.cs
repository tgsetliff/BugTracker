using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketViewModel
    {
        public int TicketId { get; set; }
        public string TicketTitle { get; set; }
        public DateTimeOffset TicketCreated { get; set; }
        public string TicketProject { get; set; }
        public string TicketType { get; set; }
        public string TicketStatus { get; set; }
        public string TicketPriority { get; set; }
        public string TicketOwnerUser { get; set; }
        public string TicketAssignedToUser { get; set; }
        public DateTimeOffset TicketUpdated { get; set; }

        public TicketViewModel(Ticket ticket)
        {
            this.TicketId = ticket.Id;
            this.TicketTitle = ticket.Title;
            this.TicketCreated = ticket.CreateDate;
            this.TicketProject = ticket.Project.Name;
            this.TicketType = ticket.TicketType.Name;
            this.TicketStatus = ticket.TicketStatus.Name;
            this.TicketPriority = ticket.TicketPriority.Name;
            this.TicketOwnerUser = ticket.OwnerUser.FirstName + " " + ticket.OwnerUser.LastName;
            //this.TicketUpdated = ticket.UpdateDate.HasValue = 'True';
        }
    }
}