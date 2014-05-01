using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using FailTracker.Infrastructure.Mapping;
using FailTracker.Filters;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FailTracker.Models
{
    public class EditIssueForm : IMapFrom<Domain.Issue>
    {
        public int IssueID { get; set; }

        public string Subject { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [DisplayName("Assigned To"), DataType("UserID")]
        public string AssignedToId { get; set; }

        [DisplayName("Issue Type")]
        public IssueType IssueType { get; set; }

        public string CreatorUserName { get; set; }

        public EditIssueForm() { }
    }
}