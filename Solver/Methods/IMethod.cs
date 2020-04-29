using Solver.Data;
using System;
using System.Collections.Generic;

namespace Solver.Methods
{
    public interface IMethod
    {
        IMethodOptions Prepare(IMethodOptions options);

        (List<int> jobOrder, int minTardiness) Solve(IMethodOptions options, System.Diagnostics.Stopwatch stopwatch);
    }
}
