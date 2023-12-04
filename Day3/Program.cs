using System.ComponentModel;

namespace Day3
{
    internal class Program
    {
        public const string InputFile = "input.txt";

        static void Main(string[] args)
        {
            int part1Solution = Part1();
            Console.WriteLine($"Solution for part1: {part1Solution}");

            int part2Solution = Part2();
            Console.WriteLine($"Solution for part2: {part2Solution}");
        }

        private static int Part1()
        {
            string[] lines = File.ReadAllLines(InputFile);
            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                int? start = null;
                int end = 0;
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char c = lines[i][j];
                    if (char.IsDigit(c))
                    {
                        if (start == null)
                            start = j;
                        end = j;
                    }
                    else
                    {
                        if (start != null)
                        {
                            sum += ProcessNumber(lines, i, start.Value, end);
                            start = null;
                        }
                    }
                }

                if (start != null)
                {
                    sum += ProcessNumber(lines, i, start.Value, end);
                }
            }

            return sum;
        }

        private static int ProcessNumber(string[] lines, int line, int start, int end)
        {
            if (!HasNeighbouringSymbol(lines, line, start, end))
                return 0;

            return int.Parse(lines[line].Substring(start, end - start + 1));
        }

        private static bool HasNeighbouringSymbol(string[] lines, int line, int start, int end)
        {
            for (int i = Math.Max(0, line - 1); i <= Math.Min(line + 1, lines.Length - 1); i++)
            {
                for (int j = Math.Max(0, start - 1); j <= Math.Min(end + 1, lines[0].Length - 1); j++)
                {
                    char c = lines[i][j];
                    if (!char.IsDigit(c) && c != '.')
                        return true;
                }
            }

            return false;
        }

        private static int Part2()
        {
            string[] lines = File.ReadAllLines(InputFile);
            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char c = lines[i][j];
                    if (c == '*')
                    {
                        sum += GetGearRatio(lines, i, j);
                    }
                }
            }

            return sum;
        }

        private static int GetGearRatio(string[] lines, int line, int position)
        {
            int product = 1;
            int numbersCount = 0;

            for (int i = Math.Max(0, line - 1); i <= Math.Min(line + 1, lines.Length - 1); i++)
            {
                bool wasDigit = false;
                for (int j = Math.Max(0, position - 1); j <= Math.Min(position + 1, lines[0].Length - 1); j++)
                {
                    char c = lines[i][j];
                    if (char.IsDigit(c))
                    {
                        if (!wasDigit)
                        {
                            numbersCount++;
                            product *= ParseNumber(lines[i],  j);
                        }

                        wasDigit = true;
                    }
                    else
                    {
                        wasDigit = false;
                    }
                }
            }

            if (numbersCount != 2)
                return 0;

            return product;
        }

        private static int ParseNumber(string line, int position)
        {
            int start = position;
            int end = position;

            while (start - 1 >= 0)
            {
                if (char.IsDigit(line[start - 1]))
                    start--;
                else
                    break;
            }

            while (end + 1 < line.Length)
            {
                if (char.IsDigit(line[end + 1]))
                    end++;
                else
                    break;
            }

            return int.Parse(line.Substring(start, end - start + 1));
        }
    }
}