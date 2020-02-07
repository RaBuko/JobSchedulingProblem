using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp.UserInterface.ConsoleGui
{
    class ConsoleHelper
    {
        public static int GetInputInt(int? min = null, int? max = null, bool allowEmpty = false)
        {
            while (true)
            {
                var input = "";
                Console.Write("Podaj odpowiedź: ");
                input = Console.ReadLine();

                if (allowEmpty && string.IsNullOrWhiteSpace(input)) { return 0; }

                if (!int.TryParse(input, out int result))
                {
                    Console.WriteLine("Podany wybór nie jest liczbą");
                    continue;
                }

                if (min.HasValue && min > result || max.HasValue && max < result)
                {
                    Console.WriteLine($"Podany wybór wykracza poza zakres");
                    continue;
                }
                return result;
            }
        }


    }
}
