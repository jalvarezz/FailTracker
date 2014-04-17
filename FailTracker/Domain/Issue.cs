using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Domain
{
    public class Issue
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public Issue() { }

        public Issue(ApplicationUser createdBy, string subject, string body)
        {
            CreatedBy = createdBy;
            Subject = subject;
            Body = body;
            CreatedAt = DateTime.Now;
        }
    }
}