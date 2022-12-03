using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Days
    {
        public static void RunDay3()
        {
            var lines = File.ReadAllLines("./Data/Day3.txt");
            var priorList = new List<int>();

            // Lowercase item types a through z have priorities 1 through 26.
            // Uppercase item types A through Z have priorities 27 through 52.

            // PART 2

            for (int i = 0; i < lines.Length; i += 3)
            {
                var exists1 = lines[i].Where(x => lines[i+1].Contains(x) && lines[i + 2].Contains(x))
                    .Select(x => x)
                    .ToList();

                if (exists1.Count == 0)
                {
                    throw new Exception("Bad check"); // FIX program if will be throw
                }
                else
                {
                    priorList.Add(Prior(exists1[0]));
                }
            }

            // PART 1
            //foreach (var line in lines)
            //{
            //    var firstPart = line.Substring(0, line.Length / 2);
            //    var secondPart = line.Substring((line.Length / 2));
            //    Console.WriteLine($"{firstPart} | {secondPart}");

            //    for (int i = 0; i < firstPart.Length; i++)
            //    {
            //        var indexSame = secondPart.IndexOf(firstPart[i]);
            //        if (indexSame >= 0)
            //        {
            //            int prior = Prior(secondPart[indexSame]);
            //            Console.WriteLine($"{secondPart[indexSame]} - {prior}");
            //            priorList.Add(prior);
            //            break;
            //        }
            //    }
            //}

            Console.WriteLine(priorList.Sum());
        }

        public static int Prior(char value)
        {
            if (char.IsLower(value))
            {
                return ((int)value) - 96;
            }
            else
            {
                return ((int)value) - 38;
            }
        }

        public static void RunDay1()
        {
            var lines = File.ReadAllLines("./Data/Day1.txt");
            var totalCaloriesByElf = new List<int>();
            int calories = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    // Elf list ends go to next
                    totalCaloriesByElf.Add(calories);
                    calories = 0;
                    continue;
                }
                calories += int.Parse(lines[i]);
            }
            totalCaloriesByElf.Add(calories);

            var maxCalories = totalCaloriesByElf.Max();
            var maxCaloriesTop3Elfs = totalCaloriesByElf.OrderByDescending(x => x).Take(3).Sum();
            Console.WriteLine(maxCalories);
            Console.WriteLine($"Top 3 sum: {maxCaloriesTop3Elfs}");
        }

        public static void RunDay2()
        {
            // A for Rock, B for Paper, and C for Scissors
            // shape you selected (1 for Rock, 2 for Paper, and 3 for Scissors
            // X for Rock, Y for Paper, and Z for Scissors

            var lines = File.ReadAllLines("./Data/Day2.txt");
            int totalScore = 0;
            int opponentChoose = 0;
            int yourChoose = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                
                switch (lines[i][0])
                {
                    case 'A':
                        opponentChoose = 1;
                        break;
                    case 'B':
                        opponentChoose = 2;
                        break;
                    case 'C':
                        opponentChoose = 3;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (lines[i][2])
                {
                    case 'X':
                        yourChoose = 1;
                        break;
                    case 'Y':
                        yourChoose = 2;
                        break;
                    case 'Z':
                        yourChoose = 3;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // minus my Choose
                // 1,2 -1 WIN
                // 1,3 -2 LOSE
                // 2,1 1 LOSE
                // 2,3 -1 WIN
                // 3,1 2 WIN
                // 3,2 1 LOSE

                // Override for Part2
                yourChoose = GiveMeMyNewChoose(yourChoose, opponentChoose);

                switch (opponentChoose - yourChoose)
                {
                    case 0: // DRAW
                        totalScore += yourChoose + 3; 
                        break;
                    case -1: // WIN
                    case 2:
                        totalScore += yourChoose + 6;
                        break;
                    case -2: // LOSE
                    case 1:
                        totalScore += yourChoose;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            Console.WriteLine(totalScore);
        }

        // // X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win
        private static int GiveMeMyNewChoose(int yourChoose, int opponentChoose)
        {
            // opponentChoose minus my Choose
            // 1-2 = -1 WIN
            // 2-3 = -1 WIN
            // 3-1 = 2 WIN

            // 1-3 = -2 LOSE
            // 2-1 = 1 LOSE
            // 3-2 = 1 LOSE

            switch (yourChoose)
            {
                case 1: // LOSE
                    return opponentChoose == 1 ? 3 : (opponentChoose - 1);
                case 2: // DRAW
                    return opponentChoose;
                case 3: // WIN
                    return opponentChoose == 3 ? 1 : (opponentChoose + 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
