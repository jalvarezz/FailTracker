using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Models
{
    public class AssignmentStatsViewModel
    {
        public string UserName { get; set; }
        public int Enhancements { get; set; }
        public int Bugs { get; set; }
        public int Support { get; set; }
        public int Other { get; set; }
    }
}