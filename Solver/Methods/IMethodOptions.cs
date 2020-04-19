using Solver.Data;
using Solver.Utils;
using System.Collections.Generic;
using System.Threading;

namespace Solver.Methods
{
    public interface IMethodOptions
    {
        List<Job> Data { get; set; }

        GuiConnection GuiConnection { get; set; }

        CancellationToken CancellationToken { get; set; }

        [UserDefined("Pokaż szczegóły", typeof(bool), true)]
        bool ShouldLogText { get; set; }

        [UserDefined("Pokaż zadania graficznie", typeof(bool), true)]
        bool ShouldLogGraphics { get; set; }
    }
}
