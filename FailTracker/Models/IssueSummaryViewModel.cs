using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Models
{
    public class IssueSummaryViewModel
    {
        public int IssueID { get; set; }
        public string Subject { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string Creator { get; set; }
        public IssueType Type { get; set; }

        public IssueSummaryViewModel() { }
    }
}