using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Utils
{
    static class StringExtension
    {
        public static string ReplaceAt(this string baseString, int position, char character)
        {
            return new StringBuilder(baseString).Remove(position, 1).Insert(position, character).ToString();
        }
    }
}
