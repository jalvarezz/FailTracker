using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FailTracker.Data;
using FailTracker.Infrastructure;

namespace FailTracker.Filters
{
    public class Log : ActionFilterAttribute
    {
        private IDictionary<string, object> _parameters;
        public ApplicationDbContext Context { get; set; }
        public ICurrentUser CurrentUser { get; set; }
        public string Description { get; set; }

        public Log(string description)
        {
            Description = description;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _parameters = filterContext.ActionParameters;   
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var description = Description;

            foreach (var kvp in _parameters)
            {
                description = description.Replace("{" + kvp.Key + "}", kvp.Value.ToString());
            }

            Context.Logs.Add(new Domain.LogAction(CurrentUser.User,
                                                  filterContext.ActionDescriptor.ActionName,
                                                  filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                                                  description));

            Context.SaveChanges();
        }
    }
}