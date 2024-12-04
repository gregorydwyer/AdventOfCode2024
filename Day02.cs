using System;
using System.Linq;
using System.Threading;

namespace AdventOfCode2024
{
    public class Day02
    {
        private const string Day = "Day02";
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
                var safeReports = 0;
                while (currentLine != null)
                {
                    var split = currentLine.Split(' ');
                    var report = split.Select(int.Parse).ToArray();
                    if (CheckSafety(report))
                    {
                        safeReports++;
                    }
                    currentLine = reader.ReadLine();
                }

                Console.WriteLine("Safe Reports: " + safeReports);
            }
        }

        private static bool CheckSafety(int[] report)
        {
            var dif = report[0] - report[1];
            if (dif <= 3 && dif >= 1)
            {
                return CheckDecrease(report, 1);
            }
            if (dif >= -3 && dif <= -1)
            {
                return CheckIncrease(report, 1);
            }
            return false;
        }

        private static bool CheckSafetyWithDamper(int[] report)
        {
            var dif = report[0] - report[1];
            return CheckIncrease(report, 0, false)
                   || CheckDecrease(report, 0, false)
                   || CheckIncrease(report, 1)
                   || CheckDecrease(report, 1);
        }

        private static bool CheckDecrease(int[] report, int index, bool damperUsed = true)
        {
            var result = false;
            if (index == report.Length - 1)
            {
                return true;
            }
            var dif = report[index] - report[index + 1];
            if (dif <= 3 && dif >= 1)
            {
                 result = CheckDecrease(report, index + 1, damperUsed);
            }

            if (result)
            {
                return true;
            }

            if (damperUsed)
            {
                return false;
            }

            if (index < report.Length - 2)
            {
                dif = report[index] - report[index + 2];
                if (dif <= 3 && dif >= 1)
                {
                    return CheckDecrease(report, index + 2);
                }
            }
            else if (index == report.Length - 2)
            {
                return true;
            }

            return false;
        }

        private static bool CheckIncrease(int[] report, int index, bool damperUsed = true)
        {
            var result = false;
            if (index == report.Length - 1)
            {
                return true;
            }
            var dif = report[index] - report[index + 1];
            if (dif >= -3 && dif <= -1)
            {
                result = CheckIncrease(report, index + 1, damperUsed);
            }

            if (result)
            {
                return true;
            }

            if (damperUsed)
            {
                return false;
            }

            if (index < report.Length - 2)
            {
                dif = report[index] - report[index + 2];
                if (dif >= -3 && dif <= -1)
                {
                    return CheckIncrease(report, index + 2);
                }
            }
            else if (index == report.Length - 2)
            {
                return true;
            }

            return false;
        }
        public static void Problem2()
        {
            Console.WriteLine(Day + " P2");
            using (var reader = Program.GetReader(FileLocation))
            {
                var currentLine = reader.ReadLine();
                var safeReports = 0;
                var rn = 0;
                while (currentLine != null)
                {
                    var split = currentLine.Split(' ');
                    var report = split.Select(int.Parse).ToArray();
                    if (CheckSafetyWithDamper(report))
                    {
                        safeReports++;
                        //Console.WriteLine("Report " + rn + " is safe. [" + currentLine + "]");
                    }
                    else
                    {
                        //Console.WriteLine("Report " + rn + " is unsafe. [" + currentLine + "]");
                    }

                    rn++;
                    currentLine = reader.ReadLine();
                }

                Console.WriteLine("Safe Reports: " + safeReports);
            }
        }
    }
}