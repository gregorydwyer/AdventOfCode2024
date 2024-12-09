using System;
using System.IO;

namespace AdventOfCode2024
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Day06.Run();
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
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
                throw e;
            }
        }
    }
}
