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
        public string Title { get; set; }
        public string Created { get; set; }
        public string Project { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string OwnerUser { get; set; }
        public string AssignedToUser { get; set; }
        public string Updated { get; set; }

      
        public TicketViewModel(Ticket ticket)
        {
            this.Title = "<a href=\"/Tickets/Details/" + ticket.Id + "\">" + ticket.Title + "</a>";
            this.Created = ticket.CreateDate.ToString("d");
            this.Project = "<a href=\"/Projects/Details/" + ticket.Project.Id + "\">" + ticket.Project.Name + "</a>"; ;
            this.Type = ticket.TicketType.Name;
            this.Status = ticket.TicketStatus.Name;
            this.Priority = ticket.TicketPriority.Name;            
            this.OwnerUser = ticket.OwnerUser.FirstName + " " + ticket.OwnerUser.LastName;
            if(ticket.AssignedToUserId != null)
            {
                this.AssignedToUser = ticket.AssignedToUser.FirstName + " " + ticket.AssignedToUser.LastName;
            }
            else
            {
                this.AssignedToUser = null;
            }
            if(ticket.UpdateDate != null)
                this.Updated = ticket.UpdateDate.HasValue ? ticket.UpdateDate.Value.Month +"/" + ticket.UpdateDate.Value.Day + "/" + ticket.UpdateDate.Value.Year : null; ;

            
        }
    }
}