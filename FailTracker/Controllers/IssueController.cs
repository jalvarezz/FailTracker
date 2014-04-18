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
            return View(_context.Issues.ToList());
        }

        // GET: /Issue/Details/5
        [Log("Viewed issue {id}")]
        public ActionResult Details(int? id)
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

        // GET: /Issue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Created issue")]
        public ActionResult Create([Bind(Include="Id,Subject,Body")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Issues.Add(new Issue(_currentUser.User, issue.Subject, issue.Body));

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
            Issue issue = _context.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: /Issue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Log("Edited issue")]
        public ActionResult Edit([Bind(Include = "Id,Subject,Body")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                Issue issueToEdit = _context.Issues.FirstOrDefault(w => w.Id == issue.Id);
                issueToEdit.Subject = issue.Subject;
                issueToEdit.Body = issue.Body;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
