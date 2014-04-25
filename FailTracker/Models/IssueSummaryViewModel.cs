using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FailTracker.Infrastructure.Mapping;

namespace FailTracker.Models
{
    public class IssueSummaryViewModel : IMapFrom<Issue>
    {
        public int IssueID { get; set; }
        public string Subject { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatorUserName { get; set; }
        public string AssignedToUserName { get; set; }
        public IssueType IssueType { get; set; }

        public IssueSummaryViewModel() { }
    }
}