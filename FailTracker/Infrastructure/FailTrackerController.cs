using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using FailTracker.Filters;

namespace FailTracker.Infrastructure
{
    [IssueTypeSelectListPopulatorAttribute, UserSelectListPopulatorAttribute]
    public abstract class FailTrackerController : Controller
    {
        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action) 
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction<TController>(this, action);
        }
    }
}