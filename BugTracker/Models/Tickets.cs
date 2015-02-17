using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Tickets
    {
        public int Id { get; set; }

        public string Title { get; set;}
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset UpdateDate { get; set; }
        public int ProjectId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }
        public int TicketStatusId { get; set; }
        public int OwnerUserId { get; set; }
        public int AssignedToUserId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual TicketTypes TicketType { get; set; }
        public virtual TicketPriorities TicketPriority { get; set; }
        public virtual TicketStatuses TicketStatus { get; set; }
        public virtual Users OwnerUser { get; set; }
        public virtual Users AssignedToUser { get; set; }

        public virtual ICollection<TicketAttachments> Attachments { get; set; }
        public virtual ICollection<TicketComments> Comments { get; set; }
        public virtual ICollection<TicketHistories> History { get; set; }
        public virtual ICollection<TicketNotifications> Notifications { get; set; }

         public Tickets()
        {
            Attachments = new HashSet<TicketAttachments>();
            Comments = new HashSet<TicketComments>();
            History = new HashSet<TicketHistories>();            
            Notifications = new HashSet<TicketNotifications>();
        }

    }
}