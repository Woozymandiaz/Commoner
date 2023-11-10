using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commoner
{
    internal class Enum
    {
        public enum Race
        {
            Other = 0,
            DwarfMountain = 1,
            DwarfHill = 2,
            Gnome = 3,
            Goblin = 4,
            HalfElf = 5,
            Halfling = 6,
            Human = 7
        }

        public enum Stat
        {
            None = 0,
            Strength = 1,
            Dexterity = 2,
            Constitution = 3,
            Intelligence = 4,
            Wisdom = 5,
            Charisma = 6
        }

        public enum Size
        {
            Small = 0,
            Medium = 1
        }
    }
}
