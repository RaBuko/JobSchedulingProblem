using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Data
{
    public class GuiActions
    {
        public Action<string, List<Job>, bool> Log { get; set; }
    }
}
