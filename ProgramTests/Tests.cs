using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonerGenerator;

namespace ProgramTests
{
    internal class Tests
    {
        static void Main(string[] args)
        {
            string x = "y";

            while (x != "n" && x != "N")
            {
                Console.Clear();

                Commoner commoner = new Commoner();

                commoner.SetRaceStats(CommonerGenerator.Enum.Race.Other);

                commoner.CommonerStats();

                Console.Write("continuar? (y/n): ");
                x = Console.ReadLine();
            }

            Console.ReadKey();
        }
    }
}
