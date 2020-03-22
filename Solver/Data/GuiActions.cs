using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Data
{
    public class GuiActions
    {
        public Action<string> Log { get; set; }

        public Action RefreshGraphicsAction { get; set; }

        public Action<List<Job>> ChangeData { get; set; }
    }
}
