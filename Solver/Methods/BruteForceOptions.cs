using Solver.Data;
using Solver.Utils;
using System.Collections.Generic;

namespace Solver.Methods
{
    public class BruteForceOptions : IMethodOptions
    {
        public List<Job> Data { get; set; }

        public GuiConnection GuiConnection { get; set; }
    }
}
