using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using FailTracker.Filters;
using FailTracker.ActionResults;

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

        [Obsolete("No not use the standard Json helpers to return JSON data to the client.")]
        protected JsonResult Json<T>(T data)
        {
            throw new InvalidOperationException("No not use the standard Json helpers to return JSON data to the client.");
        }

        protected StandardJsonResult JsonValidationError()
        {
            var result = new StandardJsonResult();

            foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
            {
                result.AddError(validationError.ErrorMessage);
            }

            return result;
        }

        protected StandardJsonResult JsonError(string errorMessage)
        {
            var result = new StandardJsonResult();

            result.AddError(errorMessage);

            return result;
        }

        protected StandardJsonResult<T> JsonSuccess<T>(T data)
        {
            return new StandardJsonResult<T> { Data = data };
        }
    }
}