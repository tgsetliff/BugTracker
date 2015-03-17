using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BugTracker.Models
{
    public class TicketViewModel
    {
        public int TicketId { get; set; }
        public string TicketTitle { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset TicketCreated { get; set; }
        public string TicketProject { get; set; }
        public string TicketType { get; set; }
        public string TicketStatus { get; set; }
        public string TicketPriority { get; set; }
        public string TicketOwnerUser { get; set; }
        public string TicketAssignedToUser { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? TicketUpdated { get; set; }

      
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
            if(ticket.AssignedToUserId != null)
            {
                this.TicketAssignedToUser = ticket.AssignedToUser.FirstName + " " + ticket.AssignedToUser.LastName;
            }
            else
            {
                this.TicketAssignedToUser = null;
            }
            this.TicketUpdated = ticket.UpdateDate;
        }
    }
}