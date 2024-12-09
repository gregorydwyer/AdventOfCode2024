using System;
using System.CodeDom;
using System.Linq;

namespace AdventOfCode2024
{
    public class Day07
    {
        private const string Day = "Day07";
        private static readonly string FileLocation = Day + ".txt";
        private static readonly string TestFileLocation = Day + "test.txt";

        public static void Run()
        {
            Problem1();
            Problem2();
        }

        public static void Problem1()
        {
            Console.WriteLine(Day + " P1");
            using (var reader = Program.GetReader(FileLocation))
            {
                var currentLine = reader.ReadLine();
                long total = 0;
                while (currentLine != null)
                {
                    var equation = ParseLine(currentLine);
                    if (TestEquation(equation.Total, equation.Parts, equation.Parts.Length - 1))
                    {
                        total += equation.Total;
                    }
                    currentLine = reader.ReadLine();
                }
                Console.WriteLine("Total:" + total);
            }
        }

        private static bool TestEquation(long total, int[] parts, int index)
        {
            if (index == 0)
            {
                return total == parts[0];
            }

            var result = false;
            if (total % parts[index] == 0)
            {
                result = TestEquation(total / parts[index], parts, index - 1);
            }

            if (!result)
            {
                result = TestEquation(total - parts[index], parts, index - 1);
            }

            return result;
        }

        private static bool TestEquationWithConcat(long total, int[] parts, int index)
        {
            if (index == 0 || total < 0)
            {
                return total == parts[0];
            }

            var result = false;
            if (total % parts[index] == 0)
            {
                result = TestEquationWithConcat(total / parts[index], parts, index - 1);
            }

            if (!result)
            {
                result = TestEquationWithConcat(total - parts[index], parts, index - 1);
            }

            var totalString = total.ToString();
            var partString = parts[index].ToString();
            if (!result && totalString.EndsWith(partString))
            {
                var temp = total.ToString();
                temp = temp.Substring(0, temp.Length - parts[index].ToString().Length);
                if (temp == "")
                {
                    return false;
                }
                result = TestEquationWithConcat(long.Parse(temp), parts, index - 1);
            }

            return result;
        }

        private static (long Total, int[] Parts) ParseLine(string line)
        {
            var split = line.Split(':');
            return (long.Parse(split[0]), split[1].Trim().Split(' ').Select(int.Parse).ToArray());
        }

        public static void Problem2()
        {
            Console.WriteLine(Day + " P2");
            using (var reader = Program.GetReader(FileLocation))
            {
                var currentLine = reader.ReadLine();
                long total = 0;
                while (currentLine != null)
                {
                    var equation = ParseLine(currentLine);
                    if (TestEquationWithConcat(equation.Total, equation.Parts, equation.Parts.Length - 1))
                    {
                        total += equation.Total;
                    }
                    currentLine = reader.ReadLine();
                }
                Console.WriteLine("Total:" + total);
            }
        }
    }
}