using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Day2
{
    internal class Program
    {
        public const string InputFile = "input.txt";

        static void Main(string[] args)
        {
            int part1Solution = Part1();
            Console.WriteLine($"Solution to part1: {part1Solution}");

            int part2Solution = Part2();
            Console.WriteLine($"Solution to part2: {part2Solution}");
        }

        private static int Part1()
        {
            int sum = 0;

            IEnumerable<string> lines = File.ReadLines(InputFile);
            foreach (string line in lines)
            {
                int maxRed = 0;
                int maxBlue = 0;
                int maxGreen = 0;

                ParseGameData(line, out int gameId, out string[] turns);

                foreach (string turn in turns)
                {
                    GetTurnValues(turn, out int red, out int blue, out int green);
                    maxRed = Math.Max(maxRed, red);
                    maxBlue = Math.Max(maxBlue, blue);
                    maxGreen = Math.Max(maxGreen, green);
                }

                if (maxRed <= 12 && maxBlue <= 14 && maxGreen <= 13)
                    sum += gameId;
            }

            return sum;
        }

        private static void ParseGameData(string line, out int gameId, out string[] turns)
        {
            gameId = 0;

            int colonIndex = line.IndexOf(":");
            for (int i = colonIndex - 1; i >= 0; i--)
            {
                if (!char.IsDigit(line[i]))
                {
                    gameId = int.Parse(line.Substring(i + 1, colonIndex - i - 1));
                    break;
                }
            }

            turns = line.Substring(colonIndex + 1).Split(';');
        }

        private static void GetTurnValues(string game, out int red, out int blue, out int green)
        {
            string[] parts = game.Split(',');

            red = 0;
            blue = 0;
            green = 0;

            foreach (string part in parts)
            {
                string partTrimmed = part.Trim();
                if (partTrimmed.EndsWith("red"))
                {
                    red = int.Parse(partTrimmed.Split()[0]);
                }
                else if (partTrimmed.EndsWith("blue"))
                {
                    blue = int.Parse(partTrimmed.Split()[0]);
                }
                else if (partTrimmed.EndsWith("green"))
                {
                    green = int.Parse(partTrimmed.Split()[0]);
                }
            }
        }

        private static int Part2()
        {
            int sum = 0;

            IEnumerable<string> lines = File.ReadLines(InputFile);
            foreach (string line in lines)
            {
                int maxRed = 0;
                int maxBlue = 0;
                int maxGreen = 0;

                ParseGameData(line, out int gameId, out string[] turns);

                foreach (string turn in turns)
                {
                    GetTurnValues(turn, out int red, out int blue, out int green);
                    maxRed = Math.Max(maxRed, red);
                    maxBlue = Math.Max(maxBlue, blue);
                    maxGreen = Math.Max(maxGreen, green);
                }

                sum += maxRed * maxBlue * maxGreen;
            }

            return sum;
        }
    }
}