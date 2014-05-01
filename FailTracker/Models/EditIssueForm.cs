using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using FailTracker.Infrastructure.Mapping;
using FailTracker.Filters;
using System.Web.Mvc;

namespace FailTracker.Models
{
    public class EditIssueForm : IMapFrom<Domain.Issue>, IHaveUserSelectList, IHaveIssueTypeSelectList
    {
        public int IssueID { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        [DisplayName("Assigned To")]
        public string AssignedToId { get; set; }
        public SelectListItem[] AvailableUsers { get; set; }

        [DisplayName("Issue Type")]
        public IssueType IssueType { get; set; }
        public SelectListItem[] AvailableIssueTypes { get; set; }

        public string CreatorUserName { get; set; }

        public EditIssueForm() { }
    }
}