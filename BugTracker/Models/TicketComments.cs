﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketComments
    {
        public int Id { get; set; }

        public string Comment { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreateDate { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }

        public virtual Tickets Ticket { get; set; }
        public virtual Users User { get; set; }
    }
}