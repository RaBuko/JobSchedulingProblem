using Solver.Data;
using Solver.Utils;
using System.Collections.Generic;

namespace Solver.Methods
{
    public interface IMethodOptions
    {
        List<Job> Data { get; set; }

        [UserDefined("Logowanie działania algorytmu")]
        bool LogEverything { get; set; }
    }
}
