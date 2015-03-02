using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var model = db.Project
                .Select(m => new ProjectViewModel() { ProjectId = m.Id, ProjectName = m.Name })
                .ToList();

            return View(model);           
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            //return View(project);
            var model = db.Project
                .Where(m => m.Id == id)
                .Select(m => new ProjectUserViewModel { ProjectId = m.Id, ProjectName = m.Name })
                .FirstOrDefault();


            // build list of users assigned to project
            var assignedProjectUserList = db.Users
                .Include("Projects")
                .Where(m => m.Projects.Any(p => p.Id == model.ProjectId))
                .OrderBy(u => u.UserName)
                .ToList();

            model.AssignedUsers = new MultiSelectList(assignedProjectUserList, "Id", "UserName");

            return View(model);
        }

        // GET: Projects/Create
        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Project.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            //Code to assign users to a project
            ViewBag.Message = (string)TempData["ListError"];

            var model = db.Project
                .Where(m => m.Id == id)
                .Select(m => new ProjectUserViewModel { ProjectId = m.Id, ProjectName = m.Name })
                .FirstOrDefault();

            
            // build list of users assigned to project
            var assignedProjectUserList = db.Users
                .Include("Projects")
                .Where(m => m.Projects.Any(p => p.Id == model.ProjectId))
                .OrderBy(u => u.UserName)
                .ToList();

            // build list of users not assigned to project
            var unassignedProjectUserList = db.Users
                .Include("Projects")
                .Where(m => !(m.Projects.Any(p => p.Id == model.ProjectId)))
                .ToList();
            
            model.AssignedUsers = new MultiSelectList(assignedProjectUserList, "Id", "UserName");
            model.UnAssignedUsers = new MultiSelectList(unassignedProjectUserList, "Id", "UserName");

            return View(model);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectUserViewModel model)
        {
            var db = new ApplicationDbContext();
       
            // check the model state - if it's valid, continue, 
            //  if not, return the view with the current model
            if (ModelState.IsValid)
            {
                // check the UsersIn attribute of the model - if it's NOT null, continue
                
                
                if(model.UsersIn != null)
                {
                    foreach(string userid in model.UsersIn)
                    {
                        // need to remove users from the Project Users table here
                        
                    }
                }
                if (model.UsersOut != null)
                {
                    // process UsersOut and add selsected
                    foreach (string userid in model.UsersOut)
                    {
                        // need to add users to the Project Users table
                    }
                    // we've succeeded - go back to the roles list view
                    return RedirectToAction("ListRoles");
                }
                
                if(model.UsersIn == null && model.UsersOut == null)
                {
                    TempData["ListError"] = "You must select at least one user from the list.";
                    return RedirectToAction("Edit", "Projects", new { projectid = model.ProjectId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            // if we get to this point, there's a problem - return the view with the model
            return View(model);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
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
