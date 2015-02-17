using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketAttachments
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreateDate { get; set; }
        public int UserId { get; set; }
        public string fileUrl { get; set; }

        public virtual Tickets TicketId { get; set; }
        public virtual Users UserId { get; set; }
    }
}