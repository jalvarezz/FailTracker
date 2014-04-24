using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FailTracker.Infrastructure.Mapping;

namespace FailTracker.Models
{
    public class IssueDetailsViewModel : IMapFrom<Domain.Issue>
    {
        public int IssueID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        public ApplicationUser Creator { get; set; }
        public IssueType IssueType { get; set; }

        public IssueDetailsViewModel() { }
    }
}