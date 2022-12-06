using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Days
    {
        public static void RunDay6Part2()
        {
            var line = File.ReadAllText("./Data/Day6.txt");

            // PART2
            //var line = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";

            var s = new char[14];

            for (int i = 13; i < line.Length; i++)
            {
                line.CopyTo(i - 13, s, 0, 14);

                if (s.Length == s.Distinct().Count())
                {
                    Console.WriteLine(i + 1);
                    return;
                }
            }
        }

        public static void RunDay6()
        {
            var line = File.ReadAllText("./Data/Day6.txt");
            //var line = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";

            var s = new char[4];

            for (int i = 3; i < line.Length; i++)
            {
                line.CopyTo(i - 3, s, 0, 4);

                if (s.Length == s.Distinct().Count())
                {
                    Console.WriteLine(i+1);
                    return;
                }
            }
        }

        public static void RunDay5()
        {
            var lines = File.ReadAllLines("./Data/Day5.txt");

            var helpLine = string.Empty;
            var stacksCount = 0;
            var stacksLines = new List<string>();

            var stacks = new List<Stack<char>>();

            var packAcumulator = new List<char>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("move"))
                {
                    var numbers = Regex.Matches(lines[i], "(\\d+)");
                    if (numbers.Count > 3)
                    {
                        throw new Exception("Too much");
                    }

                    packAcumulator.Clear();
                    var howMuch = int.Parse(numbers[0].ValueSpan);

                    for (int s = 0; s < howMuch; s++)
                    {
                        //stacks[int.Parse(numbers[2].ValueSpan) - 1].Push(stacks[int.Parse(numbers[1].ValueSpan) - 1].Pop());

                        // PART 2
                        // stay in the same order
                        packAcumulator.Add(stacks[int.Parse(numbers[1].ValueSpan) - 1].Pop());
                    }

                    packAcumulator.Reverse();
                    foreach (var item in packAcumulator)
                    {
                        stacks[int.Parse(numbers[2].ValueSpan) - 1].Push(item);
                    }
                }
                else if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    // Build stacks
                    stacksCount = stacksLines[0].Length / 4 + 1;

                    for (int s = 0; s < stacksCount; s++)
                    {
                        stacks.Add(new Stack<char>());
                    }

                    for (int j = stacksLines.Count - 2; j >= 0; j--)
                    {
                        for (int k = 0; k < stacksCount; k++)
                        {
                            helpLine = stacksLines[j].Substring(k * 4 + 1, 1);
                            if (!string.IsNullOrWhiteSpace(helpLine))
                            {
                                stacks[k].Push(helpLine[0]);
                            }
                        }
                    }
                }
                else
                {
                    // Add to stacks line
                    stacksLines.Add(lines[i]);
                }
            }

            foreach (var item in stacks)
            {
                Console.Write(item.Peek());
            }
            Console.WriteLine();
        }

        public static void RunDay4()
        {
            var lines = File.ReadAllLines("./Data/Day4.txt");
            var listConverted = new List<(int a1, int a2, int b1, int b2)>();

            var counter = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                //2-4,6-8
                (int a1, int a2, int b1, int b2) item = new(
                                    int.Parse(lines[i].Substring(0, lines[i].IndexOf("-"))),
                                    int.Parse(lines[i].Substring(lines[i].IndexOf("-") + 1, lines[i].IndexOf(",") - lines[i].IndexOf("-") - 1)),
                                    int.Parse(lines[i].Substring(lines[i].IndexOf(",") + 1, lines[i].LastIndexOf("-") - lines[i].IndexOf(",") - 1)),
                                    int.Parse(lines[i].Substring(lines[i].LastIndexOf("-") + 1))
                                    );
                listConverted.Add(item);

                if (item.a1 >= item.b1 && item.a2 <= item.b2)
                {
                    counter++;
                    continue;
                }
                else if (item.a1 <= item.b1 && item.a2 >= item.b2)
                {
                    counter++;
                    continue;
                }

                // PART2

                if (item.a1 >= item.b1 && item.a1 <= item.b2)
                {
                    counter++;
                    continue;
                }

                if (item.a2 >= item.b1 && item.a2 <= item.b2)
                {
                    counter++;
                    continue;
                }
            }

            Console.WriteLine(counter);
        }

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
