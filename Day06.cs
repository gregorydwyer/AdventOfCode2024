using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024
{
    public class Day06
    {
        private const string Day = "Day06";
        private static readonly string FileLocation = Day + ".txt";
        private static readonly string TestFileLocation = Day + "test.txt";
        private static Point Pos = new Point(0, 0);
        private const char Block = '#';
        private const char Visited = 'X';
        private const char GuardStart = '^';
        private static  Point StartingPoint;
        private static List<Point> Locations = new List<Point>();
        private static HashSet<Point> LoopLocations = new HashSet<Point>();
        private static HashSet<Point> Blocks = new HashSet<Point>();
        private static int MapWidth, MapHeight;

        public static void Run()
        {
            Problem1();
            Problem2();
        }

        public static void Problem1()
        {
            Console.WriteLine(Day + " P1");
            var map = BuildMap();
            MapWidth = map[0].Length;
            MapHeight = map.Length;
            var uniquePoints = 0;
            StartingPoint = new Point(Pos);
            while (MapContainsPos(map))
            {
                if (SimpleMovePos(map))
                {
                    uniquePoints++;
                }
            }

            Console.WriteLine("Unique Points: " + uniquePoints);
        }

        private static bool MapContainsPos(char[][] map)
        {
            return Pos.X >= 0 &&
                   Pos.X < map[0].Length &&
                   Pos.Y >= 0 &&
                   Pos.Y < map.Length;
        }

        private static bool IsPotentialLoop(Point p)
        {
            switch (p.Dir)
            {
                case Direction.N:
                    var north = Locations.Where(loc => loc.X == p.X && loc.Y > p.Y && loc.Dir == Direction.N);
                    if (!north.Any())
                    {
                        return Locations.Contains(p);
                    }
                    foreach (var loc in north)
                    {
                        for (int i = p.Y - 1; i > loc.Y; i--)
                        {
                            if (Blocks.Contains(new Point(p.X, i)))
                            {
                                return Locations.Contains(p);
                            }
                        }
                    }
                    break;
                case Direction.E:
                    var east = Locations.Where(loc => loc.Y == p.Y && loc.X < p.X && loc.Dir == Direction.E);
                    if (!east.Any())
                    {
                        return Locations.Contains(p);
                    }
                    foreach (var loc in east)
                    {
                        for (int i = p.X + 1; i < loc.X; i++)
                        {
                            if (Blocks.Contains(new Point(i, p.Y)))
                            {
                                return Locations.Contains(p);
                            }
                        }
                    }
                    break;
                case Direction.S:
                    var south = Locations.Where(loc => loc.X == p.X && loc.Y < p.Y && loc.Dir == Direction.N);
                    if (!south.Any())
                    {
                        return Locations.Contains(p);
                    }
                    foreach (var loc in south)
                    {
                        for (int i = p.Y + 1; i < loc.Y; i++)
                        {
                            if (Blocks.Contains(new Point(p.X, i)))
                            {
                                return Locations.Contains(p);
                            }
                        }
                    }
                    break;
                case Direction.W:
                    var west = Locations.Where(loc => loc.Y == p.Y && loc.X > p.X && loc.Dir == Direction.W);
                    if (!west.Any())
                    {
                        return Locations.Contains(p);
                    }
                    foreach (var loc in west)
                    {
                        for (int i = p.X - 1; i > loc.X; i--)
                        {
                            if (Blocks.Contains(new Point(i, p.Y)))
                            {
                                return Locations.Contains(p);
                            }
                        }
                    }
                    break;
            }

            return true;
        }

        private static bool SimpleMovePos(char[][] map)
        {
            Locations.Add(new Point(Pos));
            switch (Pos.Dir)
            {
                case Direction.N:
                    if (Pos.Y - 1 < 0)
                    {
                        Pos.Y--;
                        return false;
                    }
                    if (map[Pos.Y - 1][Pos.X] != Block)
                    {
                        Pos.Y--;
                        if (map[Pos.Y][Pos.X] != Visited)
                        {
                            map[Pos.Y][Pos.X] = Visited;
                            return true;
                        }

                        return false;
                    }
                    break;
                case Direction.E:
                    if (Pos.X + 1 >= map[0].Length)
                    {
                        Pos.X++;
                        return false;
                    }
                    if (map[Pos.Y][Pos.X + 1] != Block)
                    {
                        Pos.X++;
                        if (map[Pos.Y][Pos.X] != Visited)
                        {
                            map[Pos.Y][Pos.X] = Visited;
                            return true;
                        }

                        return false;
                    }
                    break;
                case Direction.S:
                    if (Pos.Y + 1 >= map.Length)
                    {
                        Pos.Y++;
                        return false;
                    }
                    if (map[Pos.Y + 1][Pos.X] != Block)
                    {
                        Pos.Y++;
                        if (map[Pos.Y][Pos.X] != Visited)
                        {
                            map[Pos.Y][Pos.X] = Visited;
                            return true;
                        }

                        return false;
                    }
                    break;
                case Direction.W:
                    if (Pos.X - 1 < 0)
                    {
                        Pos.X--;
                        return false;
                    }
                    if (map[Pos.Y][Pos.X - 1] != Block)
                    {
                        Pos.X--;
                        if (map[Pos.Y][Pos.X] != Visited)
                        {
                            map[Pos.Y][Pos.X] = Visited;
                            return true;
                        }

                        return false;
                    }
                    break;
            }

            Turn();
            return SimpleMovePos(map);
        }

        private static bool LoopCheckMovePos(List<Point> newLocs)
        {
            if (Locations.Contains(Pos) || newLocs.Contains(Pos))
            {
                // we've been here before. must be a loop.
                return true;
            }
            newLocs.Add(new Point(Pos));
            switch (Pos.Dir)
            {
                case Direction.N:
                    if (Pos.Y - 1 < 0)
                    {
                        return false;
                    }
                    if (!Blocks.Contains(new Point(Pos.X, Pos.Y - 1)))
                    {
                        Pos.Y--;
                        return LoopCheckMovePos(newLocs);
                    }
                    break;
                case Direction.E:
                    if (Pos.X + 1 >= MapWidth)
                    {
                        return false;
                    }
                    if (!Blocks.Contains(new Point(Pos.X + 1, Pos.Y)))
                    {
                        Pos.X++;
                        return LoopCheckMovePos(newLocs);
                    }
                    break;
                case Direction.S:
                    if (Pos.Y + 1 >= MapHeight)
                    {
                        return false;
                    }
                    if (!Blocks.Contains(new Point(Pos.X, Pos.Y + 1)))
                    {
                        Pos.Y++;
                        return LoopCheckMovePos(newLocs);
                    }
                    break;
                case Direction.W:
                    if (Pos.X - 1 < 0)
                    {
                        return false;
                    }
                    if (!Blocks.Contains(new Point(Pos.X - 1, Pos.Y)))
                    {
                        Pos.X--;
                        return LoopCheckMovePos(newLocs);
                    }
                    break;
            }

            Turn();
            return LoopCheckMovePos(newLocs);
        }

        private static void Turn()
        {
            Pos.Dir = NextDirection(Pos.Dir);
        }

        private static Direction NextDirection(Direction d)
        {
            return (Direction)(((int)d + 1) % 4);
        }

        private static char[][] BuildMap()
        {
            using (var reader = Program.GetReader(FileLocation))
            {
                var mapList = new List<string>();
                var currentLine = reader.ReadLine();
                while (currentLine != null)
                {
                    mapList.Add(currentLine);
                    var index = currentLine.IndexOf(GuardStart.ToString(), StringComparison.InvariantCulture);
                    if (index > 0)
                    {
                        Pos.X = index;
                        Pos.Y = mapList.Count - 1;
                    }

                    for (var i = 0; i < currentLine.Length; i++)
                    {
                        var loc = currentLine[i];
                        if (loc == Block)
                        {
                            Blocks.Add(new Point(i, mapList.Count - 1));
                        }
                    }

                    currentLine = reader.ReadLine();
                }

                return mapList.Select(str => str.ToCharArray()).ToArray();
            }
        }

        public static void Problem2()
        {
            Console.WriteLine(Day + " P2");
            Pos = new Point(StartingPoint);
            var loops = 0;
            //Locations = Locations.Take(1660).ToList();
            var locationCount = Locations.Count;
            var nextBlock = Locations.Last();
            for (int i = locationCount - 2; i > 0; i--)
            {
                Pos = Locations[i];
                if (Locations.Any(p => p.X == Pos.X && Pos.Y == p.Y && Pos.Dir != p.Dir))
                {
                    nextBlock = Locations[i];
                    Locations.RemoveAt(i);
                    continue;
                }
                var currentBlock = new Point(nextBlock.X, nextBlock.Y);
                nextBlock = Locations[i];
                Locations.RemoveAt(i);
                Blocks.Add(currentBlock);
                if (LoopCheckMovePos(new List<Point>()))
                {
                    loops++;
                }
                Blocks.Remove(currentBlock);
            }
            Console.WriteLine("Loop Locations: " + loops);
        }
    }

    public enum Direction
    {
        N,
        E,
        S,
        W,
    }

    public class Point : IEquatable<Point>
    {
        public int X;
        public int Y;
        public Direction Dir;

        public Point(int x, int y, Direction d = Direction.N)
        {
            X = x;
            Y = y;
            Dir = d;
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
            Dir = p.Dir;
        }

        public bool Equals(Point other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Dir == other.Dir;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ (int)Dir;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"({X},{Y}) {Dir.ToString()}";
        }
    }
}