using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FailTracker.Domain;
using FailTracker.Infrastructure.Mapping;
using FailTracker.Infrastructure.ModelMetadata;
using System.ComponentModel;
using FailTracker.Infrastructure.ModelMetadata.Attributes;

namespace FailTracker.Models
{
    public class IssueDetailsViewModel : IMapFrom<Domain.Issue>
    {
        [Render(ShowForEdit = false)]
        public int IssueID { get; set; }

        [Render(ShowForEdit = false)]
        public System.DateTime CreatedAt { get; set; }

        [ReadOnly(true)]
        public string CreatorUserName { get; set; }

        public string Subject { get; set; }
        public IssueType IssueType { get; set; }

        public string AssignedToUserName { get; set; }

        public string Body { get; set; }

        public IssueDetailsViewModel() { }
    }
}