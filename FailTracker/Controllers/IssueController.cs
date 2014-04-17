using FailTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailTracker.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        //
        // GET: /Issue/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IssueWidget()
        {
            return Content("Here's where issues will go");
        }
	}
}