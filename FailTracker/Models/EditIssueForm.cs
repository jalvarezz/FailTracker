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
        [HiddenInput]
        public int IssueID { get; set; }

        [ReadOnly(true)]
        public string CreatorUserName { get; set; }

        public string Subject { get; set; }

        public IssueType IssueType { get; set; }

        [DisplayName("Assigned To")]
        public string AssignedToUserName { get; set; }

        [Required]
        public string Body { get; set; }

        public EditIssueForm() { }
    }
}