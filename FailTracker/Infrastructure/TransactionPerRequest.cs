using FailTracker.Data;
using FailTracker.Infrastructure.Tasks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FailTracker.Infrastructure
{
    public class TransactionPerRequest : IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextBase _httpContext;

        public TransactionPerRequest(ApplicationDbContext context, HttpContextBase httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public void IRunOnEachRequest.Execute()
        {
            _httpContext.Items["_Transaction"] = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        public void IRunOnError.Execute()
        {
            _httpContext.Items["_Error"] = true;
        }

        public void IRunAfterEachRequest.Execute()
        {
            var transaction = (DbContextTransaction)_httpContext.Items["_Transaction"];

            if (_httpContext.Items["_Error"] != null)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
            }
        }
    }
}