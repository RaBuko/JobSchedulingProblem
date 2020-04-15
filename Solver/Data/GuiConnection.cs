using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Solver.Data
{
    public class GuiConnection
    {
        public Action<List<Job>> LogGraphics { get; set; }

        public Action<string> LogText { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
