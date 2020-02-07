using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Utils
{
    public static class ArrayExtension
    {
        public static int [] ReverseSubarray(this int[] array, int startPos, int endPos)
        {
            if (startPos > endPos) throw new ArgumentException();
            while (startPos < endPos)
            {
                (array[startPos], array[endPos]) = (array[endPos], array[startPos]);
                startPos += 1;
                endPos -= 1;
            }

            return array;
        }
    }
}
