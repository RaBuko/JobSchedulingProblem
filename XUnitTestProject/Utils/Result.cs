using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject
{
    public class Result
    {
        public string Method { get; set; }
        public int Size { get; set; }
        public string ExecElapsed { get; set; }
        public string PrepareElapsed { get; set; }
        public int Score { get; set; }
        public int BestFoundScore { get; set; }
        public string PercentScoreToBestScore { get; set; }
    }
}
