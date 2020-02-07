using Solver.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Methods
{
    public class DynamicProgrammingOptions : IMethodOptions
    {
        public List<Job> Data { get; set; }

        public Dictionary<string, int> Subsets { get; set; }
    }
}
