using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject
{
    public class Result
    {
        public string Method { get; set; }
        public string ExecElapsed { get; set; }
        public int Score { get; set; }
        public string PercentScoreToBestScore { get; set; }
        public string Parameters { get; set; }
    }
}
