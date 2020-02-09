using Solver.Data;
using Solver.Utils;
using System.Collections.Generic;

namespace Solver.Methods
{
    public class BruteForceOptions : IMethodOptions
    {
        public List<Job> Data { get; set; }

        [UserDefined("Logowanie działania algorytmu")]
        public bool LogEverything { get; set; }
    }
}
