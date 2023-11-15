using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using static CommonerGenerator.Enum;

namespace CommonerGenerator
{
    public class Commoner
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

            HitPoints = 4;

            Strength = 10;
            Dexterity = 10;
            Constitution = 10;
            Intelligence = 10;
            Wisdom = 10;
            Charisma = 10;

            SetSize(Race);
        }

        public Commoner(ScoreType method)
        {
            Random rnd = new Random();

            Race = (Race)rnd.Next(0, 8);

            Strength = 10;
            Dexterity = 10;
            Constitution = 10;
            Intelligence = 10;
            Wisdom = 10;
            Charisma = 10;

            RollStats(method);
            SetRaceStats(Race);
            SetSize(Race);
            SetHitPoints(Race, Size);
        }

        public Commoner(int scoreMin, int scoreMax)
        {
            Random rnd = new Random();

            Race = (Race)rnd.Next(0, 8);

            Strength = 10;
            Dexterity = 10;
            Constitution = 10;
            Intelligence = 10;
            Wisdom = 10;
            Charisma = 10;

            SetStats(scoreMin, scoreMax);
            SetRaceStats(Race);
            SetSize(Race);
            SetHitPoints(Race, Size);
        }

        public void RollStats(ScoreType method)
        {
            if(method == ScoreType.PointBuy)
            {
                Random rnd = new Random();

                int pointsLeft = 27;

                Strength = 8;
                Dexterity = 8;
                Constitution = 8;
                Intelligence = 8;
                Wisdom = 8;
                Charisma = 8;

                while (pointsLeft > 0)
                {
                    Stat stat = (Stat)rnd.Next(1, 7);
                    bool canIncrease = true;

                    switch (stat)
                    {
                        case Stat.Strength:
                            canIncrease = (Strength < 13) || (pointsLeft > 1 && Strength >= 13);
                            break;
                        case Stat.Dexterity:
                            canIncrease = (Dexterity < 13) || (pointsLeft > 1 && Dexterity >= 13);
                            break;
                        case Stat.Constitution:
                            canIncrease = (Constitution < 13) || (pointsLeft > 1 && Constitution >= 13);
                            break;
                        case Stat.Intelligence:
                            canIncrease = (Intelligence < 13) || (pointsLeft > 1 && Intelligence >= 13);
                            break;
                        case Stat.Wisdom:
                            canIncrease = (Wisdom < 13) || (pointsLeft > 1 && Wisdom >= 13);
                            break;
                        case Stat.Charisma:
                            canIncrease = (Charisma < 13) || (pointsLeft > 1 && Charisma >= 13);
                            break;
                    }

                    pointsLeft = canIncrease ? (pointsLeft - ChangeStat(stat, 1, 8, 15)) : pointsLeft;
                }
            }

            if(method == ScoreType.Roll3d6)
            {
                Dice dice = new Dice();

                Strength = dice.Roll(6) + dice.Roll(6) + dice.Roll(6);
                Dexterity = dice.Roll(6) + dice.Roll(6) + dice.Roll(6);
                Constitution = dice.Roll(6) + dice.Roll(6) + dice.Roll(6);
                Intelligence = dice.Roll(6) + dice.Roll(6) + dice.Roll(6);
                Wisdom = dice.Roll(6) + dice.Roll(6) + dice.Roll(6);
                Charisma = dice.Roll(6) + dice.Roll(6) + dice.Roll(6);
            }

            if (method == ScoreType.Roll4d6d1)
            {
                Dice dice = new Dice();

                Strength = 0;
                Dexterity = 0;
                Constitution = 0;
                Intelligence = 0;
                Wisdom = 0;
                Charisma = 0;

                for (int i = 1; i <= 6; i++)
                {
                    List<int> rolls = new List<int>();

                    rolls.Add(dice.Roll(6));
                    rolls.Add(dice.Roll(6));
                    rolls.Add(dice.Roll(6));
                    rolls.Add(dice.Roll(6));

                    rolls.Remove(rolls.Min());

                    foreach (int roll in rolls)
                    {
                        ChangeStat((Stat)i, roll, 1, 20);
                    }
                }
            }
        }

        public void SetStats(int scoreMin, int scoreMax)
        {
            Random rnd = new Random();
            int pointsLeft = 0;

            for (int i = 1; i <= 6; i++)
            {
                int points = rnd.Next(-2, 3);

                ChangeStat((Stat)i, points, scoreMin, scoreMax);

                pointsLeft -= points;
            }

            while (pointsLeft < 0)
            {
                Stat stat = (Stat)rnd.Next(1, 7);

                ChangeStat(stat, -1, scoreMin, scoreMax);

                pointsLeft += 1;
            }

            while (pointsLeft > 0)
            {
                Stat stat = (Stat)rnd.Next(0, 7);

                ChangeStat(stat, 1, scoreMin, scoreMax);

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

                Stat stat = (Stat)rnd.Next(0, 7);

                Stat stat1 = (stat != Stat.None) ? (Stat)rnd.Next(0, 7) : stat;
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

                while (stat1 == stat2 || stat2 == stat3 || stat1 == stat3)
                {
                    stat1 = (Stat)rnd.Next(1, 7);
                    stat2 = (Stat)rnd.Next(1, 7);
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

            HitPoints = race == Race.DwarfHill ? (HitPoints + 1) : HitPoints;
        }

        public int ChangeStat(Stat stat, int num, int scoreMin, int scoreMax)
        {
            int newStat = 10;

            switch (stat)
            {
                case Stat.Strength:
                    newStat = Strength + num;
                    Strength = (newStat >= scoreMin && newStat <= scoreMax) ? newStat : Strength;
                    break;
                case Stat.Dexterity:
                    newStat = Dexterity + num;
                    Dexterity = (newStat >= scoreMin && newStat <= scoreMax) ? newStat : Dexterity;
                    break;
                case Stat.Constitution:
                    newStat = Constitution + num;
                    Constitution = (newStat >= scoreMin && newStat <= scoreMax) ? newStat : Constitution;
                    break;
                case Stat.Intelligence:
                    newStat = Intelligence + num;
                    Intelligence = (newStat >= scoreMin && newStat <= scoreMax) ? newStat : Intelligence;
                    break;
                case Stat.Wisdom:
                    newStat = Wisdom + num;
                    Wisdom = (newStat >= scoreMin && newStat <= scoreMax) ? newStat : Wisdom;
                    break;
                case Stat.Charisma:
                    newStat = Charisma + num;
                    Charisma = (newStat >= scoreMin && newStat <= scoreMax) ? newStat : Charisma;
                    break;
            }

            int previousStat = newStat - num;

            return ((previousStat >= 13) ? 2 : 1);
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
    }
}
        