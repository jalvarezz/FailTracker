using FailTracker.Data;
using FailTracker.Infrastructure.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.App_Start
{
    public class SeedData : IRunAtStartup
    {
        private readonly ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            if (!_context.Users.Any())
            {
                _context.Users.Add(new Domain.ApplicationUser
                {
                    UserName = "TestUser"
                });

                _context.SaveChanges();
            }

            if (_context.Issues.Any())
            {
                var user = _context.Users.First();

                _context.Issues.Add(new Domain.Issue(user, "Test Issue 1", "Test Issue Body - Test Issue 1"));
                _context.Issues.Add(new Domain.Issue(user, "Test Issue 2", "Test Issue Body - Test Issue 2"));
                _context.Issues.Add(new Domain.Issue(user, "Test Issue 3", "Test Issue Body - Test Issue 3"));

                _context.SaveChanges();
            }
        }
    }
}