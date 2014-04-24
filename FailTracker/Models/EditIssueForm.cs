using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using FailTracker.Infrastructure.Mapping;

namespace FailTracker.Models
{
    public class EditIssueForm : IMapFrom<Domain.Issue>
    {
        public int IssueID { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        [DisplayName("Assigned To")]
        public string AssignedToUserID { get; set; }
        public IEnumerable<ApplicationUser> AvailableUsers { get; set; }

        [DisplayName("Issue Type")]
        public IssueType IssueType { get; set; }
        public IEnumerable<IssueType> AvailableIssueTypes { get; set; }

        public string Creator { get; set; }

        public EditIssueForm() { }
    }
}