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
    public class CreateIssueForm : IMapFrom<Domain.Issue>
    {
        public string Subject { get; set; }

        [DisplayName("Assigned To")]
        public string AssignedToId { get; set; }

        [Required]
        public string Body { get; set; }

        public IssueType IssueType { get; set; }

        

        public CreateIssueForm() { }
    }
}