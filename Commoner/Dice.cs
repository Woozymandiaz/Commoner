using System;
using System.Collections.Generic;

namespace CommonerGenerator
{
    public class Dice
    {
        public List<int> rolls { get; set; }

        int result { get; set; }

        public Dice()
        {
            result = 0;
        }

        public int Roll(int die)
        {
            Random roll = new Random();

            die += 1;

            return roll.Next(1, die);
        }

        public bool ValidateRoll(string roll)
        {
            return true;    
        }
    }
}
