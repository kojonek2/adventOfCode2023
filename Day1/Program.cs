using System.Text.RegularExpressions;

namespace Day1
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
            int calibrationSum = 0;

            IEnumerable<string> lines = File.ReadLines(InputFile);
            foreach (string line in lines)
            {
                int number = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        number = int.Parse(line[i].ToString()) * 10;
                        break;
                    }
                }

                for (int i = line.Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(line[i]))
                    {
                        number += int.Parse(line[i].ToString());
                        break;
                    }
                }

                calibrationSum += number;
            }

            return calibrationSum;
        }

        private static Dictionary<string, int> stringToDigit = new Dictionary<string, int>()
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
        };

        private static int Part2()
        {
            int calibrationSum = 0;


            string pattern = @"[\d]|" + string.Join('|', stringToDigit.Keys);
            Regex regex = new Regex(pattern);
            Regex regexFromRight = new Regex(pattern, RegexOptions.RightToLeft);

            IEnumerable<string> lines = File.ReadLines(InputFile);
            foreach (string line in lines)
            {
                int number = 0;

                Match leftMatch = regex.Match(line);
                if (leftMatch.Success)
                {
                    number = ParseDigit(leftMatch.Value) * 10;
                }

                Match rightMatch = regexFromRight.Match(line);
                if (rightMatch.Success)
                {
                    number += ParseDigit(rightMatch.Value);
                }

                calibrationSum += number;
            }

            return calibrationSum;
        }

        private static int ParseDigit(string digit)
        {
            if (stringToDigit.TryGetValue(digit, out int firstDigit))
                return firstDigit;
            else
                return int.Parse(digit);
        }
    }
}