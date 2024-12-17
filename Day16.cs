using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2024
{
    public class Day16
    {
        private const string Day = "Day16";
        private static readonly string FileLocation = Day + ".txt";
        private static readonly string TestFileLocation = Day + "test.txt";
        private static Location Start;
        private static Location End;

        public static void Run()
        {
            Program.WriteTitle("--- Day 16: Reindeer Maze ---");
            Problem1();
            Problem2();
        }

        public static void Problem1()
        {
            Program.WriteProblemNumber("Part One");
            var map = BuildMap();
        }

        public static void Problem2()
        {
            Program.WriteProblemNumber("Part Two");
            using (var reader = Program.GetReader(FileLocation))
            {

            }
        }

        private static Graph BuildMap()
        {
            using (var reader = Program.GetReader(FileLocation))
            {
                var map = new Graph();
                var currentLine = reader.ReadLine();
                var y = -1;
                while (currentLine != null)
                {
                    y++;
                    for (var x = 0; x < currentLine.Length; x++)
                    {
                        var c = currentLine[x];
                        map[x, y] = new Location(x, y);
                        if (c == '#')
                        {
                            map[x, y].IsWall = true;
                        }
                        else if (c == 'S')
                        {
                            Start = map[x, y];
                            Start.Cost = 0;
                            Start.Dir = Direction.E;
                        }
                        else if (c == 'E')
                        {
                            End = map[x, y];
                        }
                    }

                    currentLine = reader.ReadLine();
                }

                return map;
            }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Graph<T>
    {
        private readonly Dictionary<(int,int), T> map = new Dictionary<(int, int), T>();

        public T this[int x, int y]
        {
            get
            {
                if (map.TryGetValue((x, y), out var loc))
                {
                    return loc;
                }

                throw new ArgumentOutOfRangeException($"No value available for ({x},{y}).");
            }
            set
            {
                if (map.ContainsKey((x, y)))
                {
                    map[(x, y)] = value;
                }
                else
                {
                    map.Add((x,y), value);
                }
            }
        }
    }

    public class Location : Point, IComparable<Location>
    {
        public bool IsWall;
        public long Cost = long.MaxValue;

        public Location(int x, int y, bool isWall = false) : base(x, y)
        {
            IsWall = isWall;
        }

        public int CompareTo(Location other)
        {
            if (IsWall && other.IsWall)
            {
                return 0;
            }
            if (IsWall)
            {
                return 1;
            }
            if (Cost == other.Cost)
            {
                return 0;
            }
            return Cost < other.Cost ? -1 : 1;
        }
    }
}