using Solver.Data;
using Solver.Utils;
using System.Collections.Generic;

namespace Solver.Methods
{
    public interface IMethodOptions
    {
        List<Job> Data { get; set; }

        GuiConnection GuiConnection { get; set; }
    }
}
