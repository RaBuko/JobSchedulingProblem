using Solver.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Methods
{
    public interface IMethodOptions
    {
        List<Job> Data { get; set; }
    }
}
