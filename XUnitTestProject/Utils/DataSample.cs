using Solver.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject
{
    public class DataSample
    {
        public List<Job> Data { get; set; }
        public int? Best { get; set; }
        public string FileName { get; internal set; }
    }
}
