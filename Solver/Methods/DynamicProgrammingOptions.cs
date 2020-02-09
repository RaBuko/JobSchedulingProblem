using Solver.Data;
using Solver.Utils;
using System.Collections.Generic;

namespace Solver.Methods
{
    public class DynamicProgrammingOptions : IMethodOptions
    {
        public List<Job> Data { get; set; }

        public Dictionary<string, int> Subsets { get; set; }

        [UserDefined("Logowanie działania algorytmu")]
        public bool LogEverything { get; set; }
    }
}
