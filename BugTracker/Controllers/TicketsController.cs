using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System.Configuration;
using SendGrid;
using System.Net.Mail;

namespace BugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationDbContext Ticket = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
 
            var db = new ApplicationDbContext();
            var model = db.Tickets.ToList()
                .Select(m => new TicketViewModel(m));

            return View(model);                       
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
       
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.CreateDate = DateTimeOffset.UtcNow;
                ticket.OwnerUserId = User.Identity.GetUserId();
                
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
 
            TempData["Ticket"] = ticket;

            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                // need to check tempdata against incoming fields. If changes occured, then create history record
                if(TempData["Ticket"] != null)
                { 
                    Ticket getTicketHistory = (Ticket)TempData["Ticket"];
                    var userId = User.Identity.GetUserId();
                    var assignedUser = db.Users.FirstOrDefault(u => u.Id == ticket.AssignedToUserId).Email;

                    // check description
                    if(getTicketHistory.Description != ticket.Description)
                    {
                        db.TicketHistories.Add(new TicketHistory
                        {
                            ChangeDate = DateTimeOffset.UtcNow,
                            Property = "Description",
                            OldValue = getTicketHistory.Description,
                            NewValue = ticket.Description,
                            TicketId = ticket.Id,
                            UserId = userId
                        });

                    }

                    // check title
                    if (getTicketHistory.Title != ticket.Title)
                    {
                        db.TicketHistories.Add(new TicketHistory
                        {
                            ChangeDate = DateTimeOffset.UtcNow,
                            Property = "Title",
                            OldValue = getTicketHistory.Title,
                            NewValue = ticket.Title,
                            TicketId = ticket.Id,
                            UserId = userId

                        });
                    }

                    // check project id
                    if (getTicketHistory.ProjectId != ticket.ProjectId)
                    {
                        db.TicketHistories.Add(new TicketHistory
                        {
                            ChangeDate = DateTimeOffset.UtcNow,
                            Property = "Project",
                            OldValue = getTicketHistory.Project.Name,
                            NewValue = db.Project.FirstOrDefault(p => p.Id == ticket.ProjectId).Name,
                            TicketId = ticket.Id,
                            UserId = userId
                        });
                    }

                    // check status id
                    if (getTicketHistory.TicketStatusId != ticket.TicketStatusId)
                    {
                        db.TicketHistories.Add(new TicketHistory
                        {
                            ChangeDate = DateTimeOffset.UtcNow,
                            Property = "Status",
                            OldValue = getTicketHistory.TicketStatus.Name,
                            NewValue = db.TicketStatuses.FirstOrDefault(p => p.Id == ticket.TicketStatusId).Name,
                            TicketId = ticket.Id,
                            UserId = userId
                        });
                    }

                    // check status id
                    if (getTicketHistory.TicketTypeId != ticket.TicketTypeId)
                    {
                        db.TicketHistories.Add(new TicketHistory
                        {
                            ChangeDate = DateTimeOffset.UtcNow,
                            Property = "Type",
                            OldValue = getTicketHistory.TicketType.Name,
                            NewValue = db.TicketTypes.FirstOrDefault(p => p.Id == ticket.TicketTypeId).Name,
                            TicketId = ticket.Id,
                            UserId = userId
                        });
                    }

                    // check priority id
                    if (getTicketHistory.TicketPriorityId != ticket.TicketPriorityId)
                    {
                        db.TicketHistories.Add(new TicketHistory
                        {
                            ChangeDate = DateTimeOffset.UtcNow,
                            Property = "Priority",
                            OldValue = getTicketHistory.TicketPriority.Name,
                            NewValue = db.TicketPriorities.FirstOrDefault(p => p.Id == ticket.TicketPriorityId).Name,
                            TicketId = ticket.Id,
                            UserId = userId
                        });
                    }               

                    // check assigned to user, if changed, notify new asignee
                    if (getTicketHistory.AssignedToUserId != ticket.AssignedToUserId)
                    {
                        // update history
                        db.TicketHistories.Add(new TicketHistory
                        {
                            ChangeDate = DateTimeOffset.UtcNow,
                            Property = "Assigned To",
                            OldValue = getTicketHistory.AssignedToUser.FirstName + " " + getTicketHistory.AssignedToUser.LastName,
                            NewValue = ticket.AssignedToUser.FirstName + " " + ticket.AssignedToUser.LastName,
                            TicketId = ticket.Id,
                            UserId = userId
                        });

                        var MyAddress = ConfigurationManager.AppSettings["ContactEmail"];
                        var MyUsername = ConfigurationManager.AppSettings["Username"];
                        var MyPassword = ConfigurationManager.AppSettings["Password"];
                        var link = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + Url.Action("Details", "Tickets", new { id = ticket.Id });

                        SendGridMessage mail = new SendGridMessage();
                        mail.From = new MailAddress("noreply@bugtracker.com");
                        mail.AddTo(assignedUser);
                        mail.Subject = "New Ticket Assignment for ticket: " + ticket.Title;
                        mail.Text = "You have been assigned a new Ticket. Please review at your earliest opportunity.  " + link;
                        var credentials = new NetworkCredential(MyUsername, MyPassword);
                        var transportWeb = new Web(credentials);
                        transportWeb.Deliver(mail);

                        db.TicketNotifications.Add(new TicketNotification
                        {
                            TicketId = ticket.Id,
                            UserId = userId
                        });
                    }
                }               

                ticket.UpdateDate = DateTimeOffset.UtcNow;
                //ticket.AssignedToUserId = User.Identity.GetUserId();

                db.Entry(ticket).State = EntityState.Modified;
                db.Entry(ticket).Property(p => p.OwnerUserId).IsModified = false;
                db.Entry(ticket).Property(p=> p.CreateDate).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // add a comment
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment([Bind(Include = "TicketId, Comment")] string Slug, TicketComment ticketComment)
        {
            if (ModelState.IsValid)
            {
                if (ticketComment.Comment != null)
                {
                    ticketComment.UserId = User.Identity.GetUserId();
                    ticketComment.CreateDate = DateTimeOffset.UtcNow;
                    db.TicketComments.Add(ticketComment);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Details", new { id = ticketComment.TicketId });
        }

        // GET: Comment Details
        public ActionResult CommentDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment comment = db.TicketComments.Find(id);
            if(comment == null)
            {
                return HttpNotFound();
            }
            
            return View(comment);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
