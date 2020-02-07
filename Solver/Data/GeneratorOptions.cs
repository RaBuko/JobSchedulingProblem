using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Data
{
    public class GeneratorOptions
    {
        public int JobsCount { get; set; }
        public int? MaxTerm { get; set; }
        public int? MaxTime { get; set; }
        public int? MaxWeight { get; set; }
    }
}
