using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FailTracker.Data;
using Microsoft.AspNet.Identity;
using FailTracker.Domain;
using FailTracker.Filters;
using FailTracker.Infrastructure;
using FailTracker.Models;
using AutoMapper.QueryableExtensions;

namespace FailTracker.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public IssueController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: /Issue/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Issue/Details/5
        [Log("Viewed issue {id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var issue = _context.Issues
                .Include(i => i.AssignedTo)
                .Include(i => i.Creator).Project().To<IssueDetailsViewModel>()
                .SingleOrDefault(i => i.IssueID == id);

            if (issue == null)
            {
                return HttpNotFound();
            }

            return View(issue);
        }

        // GET: /Issue/Create
        public ActionResult Create()
        {
            return View(new CreateIssueForm
            {
                AvailableUsers = GetAvailableUsers(),
                AvailableIssueTypes = GetAvailableIssueTypes()
            });
        }

        // POST: /Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Created issue")]
        public ActionResult Create([Bind(Include = "Subject,Body,IssueType,AssignedToUserID")] CreateIssueForm issue)
        {
            if (ModelState.IsValid)
            {
                var assignedUser = _context.Users.FirstOrDefault(r => r.Id == issue.AssignedToUserID);
                var newIssue = new Issue(_currentUser.User, issue.Subject, issue.Body, assignedUser, issue.IssueType);

                _context.Issues.Add(newIssue);

                if (assignedUser.Assignments == null) assignedUser.Assignments = new List<Issue>();

                assignedUser.Assignments.Add(newIssue);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(issue);
        }

        // GET: /Issue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _context.Issues
                .Include(i => i.AssignedTo)
                .Include(i => i.Creator).Project().To<EditIssueForm>()
                .SingleOrDefault(i => i.IssueID == id);

            if (model == null)
            {
                return HttpNotFound();
            }

            model.AvailableUsers = GetAvailableUsers();
            model.AvailableIssueTypes = GetAvailableIssueTypes();

            return View(model);
        }

        // POST: /Issue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Edited issue")]
        public ActionResult Edit([Bind(Include = "IssueID,Subject,Body,IssueType,AssignedToUserID")] EditIssueForm issue)
        {
            if (ModelState.IsValid)
            {
                Issue issueToEdit = _context.Issues.FirstOrDefault(w => w.IssueID == issue.IssueID);
                issueToEdit.Subject = issue.Subject;
                issueToEdit.Body = issue.Body;
                issueToEdit.AssignedTo = _context.Users.FirstOrDefault(r => r.Id == issue.AssignedToUserID);
                issueToEdit.IssueType = issue.IssueType;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: /Issue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = _context.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }

            return View(issue);
        }

        // POST: /Issue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken, Log("Deleted issue {id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = _context.Issues.Find(id);
            _context.Issues.Remove(issue);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private IEnumerable<IssueType> GetAvailableIssueTypes()
        {
            return new List<IssueType>() { 
                IssueType.Bug,
                IssueType.Enhancement,
                IssueType.Support,
                IssueType.Other
            };
        }

        private IEnumerable<ApplicationUser> GetAvailableUsers()
        {
            return _context.Users.ToList();
        }


        [ChildActionOnly]
        public ActionResult AssignmentStatsWidget()
        {
            var models = _context.Users.Project().To<AssignmentStatsViewModel>();

            return PartialView(models);
        }

        [ChildActionOnly]
        public ActionResult CreatedByYouWidget()
        {
            var models = _context.Issues.Include(r => r.Creator).Include(r => r.AssignedTo).Where(r => r.Creator.Id == _currentUser.User.Id).Project().To<IssueSummaryViewModel>();

            return PartialView(models.ToArray());
        }

        [ChildActionOnly]
        public ActionResult YourIssuesWidget()
        {
            var models = _context.Issues.Include(r => r.Creator).Include(r => r.AssignedTo).Where(r => r.AssignedTo.Id == _currentUser.User.Id).Project().To<IssueSummaryViewModel>();

            return PartialView(models.ToArray());
        }
    }
}
