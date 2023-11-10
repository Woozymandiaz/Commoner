using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commoner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Commoner commoner = null;
            bool executing = true;
            string response = "y";

            while (executing)
            {
                Console.Clear();

                commoner = new Commoner();
                commoner.CommonerStats();

                Console.Write("Roll again? (y/n): ");

                response = Console.ReadLine();

                if (response == "n" || response == "N")
                    executing = false;
            }

            Console.WriteLine();
            Console.Write("Program finished! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
