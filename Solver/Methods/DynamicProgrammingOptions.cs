using Solver.Data;
using Solver.Utils;
using System.Collections.Generic;
using System.Threading;

namespace Solver.Methods
{
    public class DynamicProgrammingOptions : IMethodOptions
    {
        public List<Job> Data { get; set; }

        public GuiConnection GuiConnection { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public Dictionary<string, int> Subsets { get; set; }
    }
}
