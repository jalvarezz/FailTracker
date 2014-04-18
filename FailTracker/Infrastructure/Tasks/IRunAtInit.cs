using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FailTracker.Infrastructure.Tasks
{
    public interface IRunAtInit
    {
        void Execute();
    }
}
