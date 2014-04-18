using FailTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Infrastructure
{
    public interface ICurrentUser
    {
        ApplicationUser User { get; }
    }
}