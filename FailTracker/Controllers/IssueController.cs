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
using Microsoft.Web.Mvc;
using FailTracker.Infrastructure.Alerts;

namespace FailTracker.Controllers
{
    public class IssueController : FailTrackerController
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
            return View(new CreateIssueForm());
        }

        // POST: /Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Created issue")]
        public ActionResult Create(CreateIssueForm model)
        {
            if (ModelState.IsValid)
            {
                var assignedUser = _context.Users.Single(r => r.Id == model.AssignedToId);
                var newIssue = new Issue(_currentUser.User, model.Subject, model.Body, assignedUser, model.IssueType);

                _context.Issues.Add(newIssue);

                if (assignedUser.Assignments == null) assignedUser.Assignments = new List<Issue>();

                assignedUser.Assignments.Add(newIssue);

                _context.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction<IssueController>(x => x.Index()).WithSuccess("Issue Created!");
            }

            return View(model);
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
                //return HttpNotFound();
                return RedirectToAction<IssueController>(x => x.Index()).WithError("Unable to find the issue. Maybe it was deleted?");
            }

            return View(model);
        }

        // POST: /Issue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [/*CustomAntiForgeryToken, */Log("Saving changes")]
        public ActionResult Edit(EditIssueForm issue)
        {
            if (!ModelState.IsValid)
            {
                return JsonValidationError();
            }

            Issue issueToEdit = _context.Issues.Single(w => w.IssueID == issue.IssueID);

            if (issueToEdit == null)
            {
                return JsonError("Cannot find the issue specified.");
            }

            issueToEdit.Subject = issue.Subject;
            issueToEdit.Body = issue.Body;
            issueToEdit.AssignedTo = _context.Users.Single(r => r.UserName == issue.AssignedToUserName);
            issueToEdit.IssueType = issue.IssueType;

            _context.Entry<Issue>(issueToEdit).State = EntityState.Modified;

            if (_context.SaveChanges() > 0)
                return JsonSuccess(issue);
            else
                return JsonError("Issue could not be saved");
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
                //return HttpNotFound();
                return RedirectToAction<IssueController>(x => x.Index()).WithError("Unable to find the issue. Maybe it was deleted?");
            }

            return View(issue);
        }

        // POST: /Issue/Delete/5
        [HttpPost, ActionName("Delete")]
        [/*ValidateAntiForgeryToken,*/ Log("Deleted issue {id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = _context.Issues.Find(id);
            _context.Entry<Issue>(issue).State = EntityState.Deleted;

            if (issue == null)
            {
                //return HttpNotFound();
                return RedirectToAction<IssueController>(x => x.Index()).WithError("Unable to find the issue. Maybe it was deleted?");
            }

            if (_context.SaveChanges() > 0)
                return RedirectToAction<IssueController>(x => x.Index()).WithSuccess("Issue successfully deleted!");
            else
                return RedirectToAction<IssueController>(x => x.Details(id)).WithError("Issue could not be saved!");
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
