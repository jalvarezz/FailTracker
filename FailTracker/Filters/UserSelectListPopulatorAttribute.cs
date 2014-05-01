using FailTracker.Data;
using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailTracker.Filters
{
    public class UserSelectListPopulatorAttribute : ActionFilterAttribute
    {
        public ApplicationDbContext Context { get; set; }

        private SelectListItem[] GetAvailableUsers()
        {
            return Context.Users.Select(u => new SelectListItem
                       {
                           Text = u.UserName,
                           Value = u.Id
                       }).ToArray();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult != null && viewResult.Model is IHaveUserSelectList)
            {
                ((IHaveUserSelectList)viewResult.Model).AvailableUsers = GetAvailableUsers();
            }
        }
    }

    public interface IHaveUserSelectList
    {
        SelectListItem[] AvailableUsers { get; set; }
    }
}