using System;
using System.IO;

namespace AdventOfCode2024
{
    public class Program
    {
        private static readonly int[] Notes = { 392, 392, 392, 350, 330, 330, 294, 294, 262, 262, 294, 294, 330, 330, 262, 262,
                                                294, 330, 350, 294, 330, 330, 330, 294, 262, 262, 247, 247, 262, 262, 262, 262};
        private static int NoteIndex = 0;
        private static void Main(string[] args)
        {
            Day01.Run();
            Day02.Run();
            Day03.Run();
            Day04.Run();
            Day05.Run();
            Day06.Run();
            Day07.Run();
            Day08.Run();
            Day09.Run();
            Day10.Run();
            Day11.Run();
            Day12.Run();
            Day13.Run();
            Day14.Run();
            Day15.Run();
            while (NoteIndex != 0)
            {
                Console.Beep(Notes[NoteIndex], 250);
                NoteIndex = (NoteIndex + 1) % Notes.Length;
            }
            Console.ReadKey();
        }

        public static StreamReader GetReader(string fileLocation)
        {
            try
            {
                var reader = new StreamReader(new FileStream(fileLocation, FileMode.Open, FileAccess.Read));
                return reader;
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
                throw e;
            }
        }

        public static void WriteTitle(string title)
        {
            Console.Beep(Notes[NoteIndex], 300);
            NoteIndex = (NoteIndex + 1) % Notes.Length;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(title);
        }

        public static void WriteProblemNumber(string problemNumber)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(problemNumber);
        }

        public static void WriteOutput(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(output);
        }
    }
}
