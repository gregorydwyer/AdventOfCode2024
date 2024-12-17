using System;
using System.Collections.Generic;

namespace AdventOfCode2024
{
    public class Day17
    {
        private const string Day = "Day17";
        private static readonly string FileLocation = Day + ".txt";
        private static readonly string TestFileLocation = Day + "test.txt";
        private static int A, B, C;
        private static int Pointer;
        private static List<int> Output = new List<int>();

        public static void Run()
        {
            Program.WriteTitle("--- Day 17: Chronospatial Computer ---");
            Problem1();
            Problem2();
        }

        public static void Problem1()
        {
            Program.WriteProblemNumber("Part One");
            var program = BuildProgram();
            while (Pointer < program.Count)
            {
                RunOpCode(program[Pointer], program[Pointer + 1]);
            }
            Program.WriteOutput("Final Output: " + string.Join(",", Output));
        }

        public static void Problem2()
        {
            Program.WriteProblemNumber("Part Two");
            var program = BuildProgram();
            var programString = string.Join(",", program);
            var match = false;
            var nextA = -1;
            while (!match)
            {
                nextA++;
                A = nextA;
                B = C = 0;
                Pointer = 0;
                Output.Clear();
                
                while(Pointer < program.Count)
                {
                    RunOpCode(program[Pointer], program[Pointer + 1]);
                    if(!programString.StartsWith(string.Join(",", Output)))
                    {
                        break;
                    }
                }

                match = programString.Equals(string.Join(",", Output));
                if (nextA % 1000000 == 0)
                {
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            Program.WriteOutput("Match when A = " + (nextA - 1));
        }

        private static void RunOpCode(int opCode, int operand)
        {
            switch (opCode)
            {
                case 0: //adv
                    A =  A / (int) Math.Pow(2, GetComboOperand(operand));
                    break;
                case 1: //bxl
                    B = B ^ operand;
                    break;
                case 2: //bst
                    B = GetComboOperand(operand) % 8;
                    break;
                case 3: //jnz
                    if(A != 0)
                    {
                        Pointer = operand;
                        return;
                    }
                    break;
                case 4: //bxc
                    B = B ^ C; 
                    break;
                case 5: //out
                    Output.Add(GetComboOperand(operand) % 8);
                    break;
                case 6: //bdv
                    B = A / (int) Math.Pow(2, GetComboOperand(operand));
                    break;
                case 7: //cdv
                    C = A / (int)Math.Pow(2, GetComboOperand(operand));
                    break;
            }
            Pointer += 2;
        }

        private static int GetComboOperand(int operand)
        {
            var value = 0;
            switch (operand)
            {
                case 4:
                    value = A;
                    break;
                case 5:
                    value = B;
                    break;
                case 6:
                    value = C;
                    break;
                case 7:
                    throw new InvalidProgramException();
                    break;
                default:
                    value = operand;
                    break;
            }
            return value;
        }

        private static List<int> BuildProgram()
        {
            using (var reader = Program.GetReader(FileLocation))
            {
                var program = new List<int>();
                var currentLine = reader.ReadLine();
                while(currentLine != null)
                {
                    if (currentLine.Contains("A"))
                    {
                        A = int.Parse(currentLine.Split(':')[1].Trim());
                    }
                    if (currentLine.Contains("B"))
                    {
                        B = int.Parse(currentLine.Split(':')[1].Trim());
                    }
                    if (currentLine.Contains("C"))
                    {
                        C = int.Parse(currentLine.Split(':')[1].Trim());
                    }
                    if (currentLine.Contains("P"))
                    {
                        var input = currentLine.Split(':')[1].Trim().Split(',');
                        foreach (var item in input)
                        {
                            program.Add(int.Parse(item));
                        }
                    }

                    currentLine = reader.ReadLine();
                }
                return program;

            }
        }
    }
}