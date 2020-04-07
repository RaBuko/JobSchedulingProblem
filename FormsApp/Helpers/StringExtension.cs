using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp.Helpers
{
    public static class StringExtension
    {
        public static string Center(this string s, int width, char paddingChar = ' ')
        {
            if (s.Length >= width)
            {
                return s;
            }

            int leftPadding = (width - s.Length) / 2;
            int rightPadding = width - s.Length - leftPadding;

            return new string(paddingChar, leftPadding) + s + new string(paddingChar, rightPadding);
        }
    }
}
