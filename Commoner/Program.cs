using System;

namespace CommonerGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool executing = true;

            while (executing)
            {
                Console.Clear();

                Commoner commoner = new Commoner(Enum.ScoreType.PointBuy);
                commoner.CommonerStats();

                Console.Write("Roll again? (y/n): ");

                string response = Console.ReadLine();

                if (response == "n" || response == "N")
                    executing = false;
            }

            Console.WriteLine();
            Console.Write("Program finished! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
