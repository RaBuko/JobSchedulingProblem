using System;

namespace Solver.Utils
{
    public static class IntManip
    {
        public static string IntToBin(this int number, int length)
        {
            return Convert.ToString(number, 2).PadLeft(length, '0');
        }
    }
}
