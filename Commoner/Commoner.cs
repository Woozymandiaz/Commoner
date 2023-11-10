using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Commoner.Enum;

namespace Commoner
{
    internal class Commoner
    {
        public Race Race { get; set; }

        public Size Size { get; set; }

        public int HitPoints { get; set; }

        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Constitution { get; set; }

        public int Intelligence { get; set; }

        public int Wisdom { get; set; }

        public int Charisma { get; set; }

        public Commoner()
        {
            Random rnd = new Random();
            Race = (Race)rnd.Next(0, 8);

            Strength = 10;
            Dexterity = 10;
            Constitution = 10;
            Intelligence = 10;
            Wisdom = 10;
            Charisma = 10;

            SetStats();
            SetRaceStats(Race);
            SetSize(Race);
            SetHitPoints(Race, Size);
        }

        public void CommonerStats()
        {
            string hitDie = "1d8";

            if (Size == Size.Small)
                hitDie = "1d6";

            int conMod = (Constitution - 10) / 2;
            if (Constitution == 9)
                conMod -= 1;

            string hitPoints = "";

            if (conMod == 0)
                hitPoints = "Hit Points: " + HitPoints + " (" + hitDie + ")";
            else if (conMod > 0)
                hitPoints = "Hit Points: " + HitPoints + " (" + hitDie + " + " + conMod + ")";
            else
                hitPoints = "Hit Points: " + HitPoints + " (" + hitDie + " - " + (-1 * conMod) + ")";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Race: " + Race.ToString());
            sb.AppendLine("Size: " + Size.ToString());
            sb.AppendLine(hitPoints);
            sb.AppendLine("Strength: " + Strength);
            sb.AppendLine("Dexterity: " + Dexterity);
            sb.AppendLine("Constitution: " + Constitution);
            sb.AppendLine("Intelligence: " + Intelligence);
            sb.AppendLine("Wisdom: " + Wisdom);
            sb.AppendLine("Charisma: " + Charisma);

            Console.WriteLine(sb.ToString());
        }

        public void SetStats()
        {
            Random rnd = new Random();
            int pointsLeft = 0;

            for (int i = 1; i <= 6; i++)
            {
                int points = rnd.Next(-2, 3);

                ChangeStat((Stat)i, points);

                pointsLeft -= points;
            }

            while (pointsLeft < 0)
            {
                Stat stat = (Stat)rnd.Next(1, 7);

                ChangeStat(stat, -1);

                pointsLeft += 1;
            }

            while (pointsLeft > 0)
            {
                Stat stat = (Stat)rnd.Next(0, 7);

                ChangeStat(stat, 1);

                pointsLeft -= 1;
            }
        }

        public void SetRaceStats(Race race)
        {
            Random rnd = new Random();

            if (race == Race.Human)
            {
                IncreaseRaceStat(Stat.Strength, 1);
                IncreaseRaceStat(Stat.Dexterity, 1);
                IncreaseRaceStat(Stat.Constitution, 1);
                IncreaseRaceStat(Stat.Intelligence, 1);
                IncreaseRaceStat(Stat.Wisdom, 1);
                IncreaseRaceStat(Stat.Charisma, 1);
            }
            else if (race == Race.DwarfMountain)
            {
                IncreaseRaceStat(Stat.Strength, 2);
                IncreaseRaceStat(Stat.Constitution, 2);
            }
            else if (race == Race.HalfElf)
            {
                Stat stat1 = Stat.None;
                Stat stat2 = Stat.None;

                stat1 = (Stat)rnd.Next(1, 6);

                do
                {
                    stat2 = (Stat)rnd.Next(1, 6);
                } while (stat2 != stat1 && stat2 != Stat.Charisma);

                IncreaseRaceStat(stat1, 1);
                IncreaseRaceStat(stat2, 1);
                IncreaseRaceStat(Stat.Charisma, 2);
            }
            else
            {
                int pointsLeft = 3;

                Stat stat1 = (Stat)rnd.Next(0, 7);
                Stat stat2 = Stat.None;
                Stat stat3 = Stat.None;

                if (stat1 == Stat.None)
                {
                    stat2 = (Stat)rnd.Next(1, 7);
                    stat3 = (Stat)rnd.Next(1, 7);
                }
                else
                {
                    stat2 = (Stat)rnd.Next(0, 7);
                    if (stat2 == Stat.None)
                        stat3 = (Stat)rnd.Next(1, 7);
                    else
                        stat3 = (Stat)rnd.Next(0, 7);
                }

                while (stat1 == stat2 && stat2 == stat3)
                {
                    stat1 = (Stat)rnd.Next(1, 7);
                }

                while (pointsLeft > 0)
                {
                    if (pointsLeft != 0 && stat1 != Stat.None)
                    {
                        IncreaseRaceStat(stat1, 1);
                        pointsLeft -= 1;
                    }

                    if (pointsLeft != 0 && stat2 != Stat.None)
                    {
                        IncreaseRaceStat(stat2, 1);
                        pointsLeft -= 1;
                    }

                    if (pointsLeft != 0 && stat3 != Stat.None)
                    {
                        IncreaseRaceStat(stat3, 1);
                        pointsLeft -= 1;
                    }
                }
            }
        }

        public void SetSize(Race race)
        {
            Random rnd = new Random();

            switch (race)
            {
                case Race.Gnome:
                case Race.Goblin:
                case Race.Halfling:
                    Size = Size.Small;
                    break;
                default:
                    Size = (Size)rnd.Next(0, 2);
                    if (Size == Size.Small)
                        Size = (Size)rnd.Next(0, 2);
                    break;
            }
        }

        public void SetHitPoints(Race race, Size size)
        {
            int conModifier = (Constitution - 10) / 2;

            if (Constitution == 9)
                conModifier -= 1;

            if (size == Size.Medium)
            {
                HitPoints = 5 + conModifier;
            }
            else
            {
                HitPoints = 4 + conModifier;
            }

            if (race == Race.DwarfHill)
                HitPoints += 1;
        }

        public void ChangeStat(Stat stat, int num)
        {
            int newStat = 10;

            switch (stat)
            {
                case Stat.Strength:
                    newStat = Strength += num;
                    if (newStat >= 8 && newStat <= 13)
                        Strength = newStat;
                    break;
                case Stat.Dexterity:
                    newStat = Dexterity += num;
                    if (newStat >= 8 && newStat <= 13)
                        Dexterity = newStat;
                    break;
                case Stat.Constitution:
                    newStat = Constitution += num;
                    if (newStat >= 8 && newStat <= 13)
                        Constitution = newStat;
                    break;
                case Stat.Intelligence:
                    newStat = Intelligence += num;
                    if (newStat >= 8 && newStat <= 13)
                        Intelligence = newStat;
                    break;
                case Stat.Wisdom:
                    newStat = Wisdom = newStat;
                    if (newStat >= 8 && newStat <= 13)
                        Wisdom += num;
                    break;
                case Stat.Charisma:
                    newStat = Charisma = newStat;
                    if (newStat >= 8 && newStat <= 13)
                        Charisma += num;
                    break;
            }
        }

        public void IncreaseRaceStat(Stat stat, int num)
        {
            switch (stat)
            {
                case Stat.Strength:
                    Strength += num;
                    break;
                case Stat.Dexterity:
                    Dexterity += num;
                    break;
                case Stat.Constitution:
                    Constitution += num;
                    break;
                case Stat.Intelligence:
                    Intelligence += num;
                    break;
                case Stat.Wisdom:
                    Wisdom += num;
                    break;
                case Stat.Charisma:
                    Charisma += num;
                    break;
            }
        }
    }
}
