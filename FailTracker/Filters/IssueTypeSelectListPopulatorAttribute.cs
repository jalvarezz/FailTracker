﻿using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailTracker.Filters
{
    public class IssueTypeSelectListPopulatorAttribute : ActionFilterAttribute
    {
        private SelectListItem[] GetAvailableIssueTypes()
        {
            return Enum.GetValues(typeof(IssueType))
                       .Cast<IssueType>()
                       .Select(t => new SelectListItem
                       {
                           Text = t.ToString(),
                           Value = t.ToString()
                       }).ToArray();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult != null && viewResult.Model is IHaveIssueTypeSelectList)
            {
                ((IHaveIssueTypeSelectList)viewResult.Model).AvailableIssueTypes = GetAvailableIssueTypes();
            }
        }
    }

    public interface IHaveIssueTypeSelectList
    {
        SelectListItem[] AvailableIssueTypes { get; set; }
    }
}