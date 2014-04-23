using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Domain
{
    public class Issue
    {
        public int IssueID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        public ApplicationUser Creator { get; set; }
        public IssueType IssueType { get; set; }

        public Issue() { }

        public Issue(ApplicationUser creator, string subject, string body, ApplicationUser assignedTo, IssueType issueType)
        {
            Creator = creator;
            Subject = subject;
            Body = body;
            CreatedAt = DateTime.Now;
            Creator = creator;
            IssueType = issueType;
            AssignedTo = assignedTo;
        }
    }

    public enum IssueType
    {
        Enhancement,
        Bug,
        Support,
        Other
    }
}