using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2023.Common;

namespace AdventOfCode2023.Problems.Day8
{
    public class Day8_HauntedWasteland
    {
        private const int InstructionsLineIndex = 0;
        private const int MapLineIndex = 2;
        private const int MapLineIndexIncrement = 1;

        private readonly string _inputPath;

        public Day8_HauntedWasteland(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var mapLineIndex = InstructionsLineIndex;
            var lines = FileOperations.ReadLines(_inputPath);
            var instructions = ProcessInstructions(mapLineIndex, lines);
            
            mapLineIndex = MapLineIndex;
            var map = ProcessMap(ref mapLineIndex, lines);
            
            var result1 = CountSteps(instructions, map);
            Console.WriteLine($"Steps: {result1}");
            
            var result2 = CountStepsToEndingZNodes(instructions, map);
            Console.WriteLine($"Steps to nodes Z: {result2}");
        }

        private char[] ProcessInstructions(int lineIndex, List<string> lines)
        {
            return lines[lineIndex].ToCharArray();
        }

        private Dictionary<string, (string, string)> ProcessMap(ref int lineIndex, List<string> lines)
        {
            var map = new Dictionary<string, (string, string)>();

            while (lineIndex < lines.Count && lines[lineIndex] != string.Empty)
            {
                var line = lines[lineIndex];
                var parsedData = Regex.Matches(line, "\\w+");
                map.Add(parsedData[0].Value, (parsedData[1].Value, parsedData[2].Value));
                lineIndex++;
            }

            lineIndex += MapLineIndexIncrement;

            return map;
        }
        
        private long CountSteps(char[] instructions, Dictionary<string, (string L, string R)> map)
        {
            var currentKey = "AAA";
            var count = 0;

            while (currentKey != "ZZZ")
            {
                var instruction = instructions[count % instructions.Length];

                switch (instruction)
                {
                    case 'L':
                        currentKey = map[currentKey].L;
                        count++;
                        break;
                    case 'R':
                        currentKey = map[currentKey].R;
                        count++;
                        break;
                    default:
                        throw new InvalidOperationException("Invalid instruction");
                }
            }

            return count;
        }
        
        private long CountStepsToEndingZNodes(char[] instructions, Dictionary<string, (string L, string R)> map)
        {
            var endingANodes = map.Keys.Where(key => key.EndsWith("A")).ToList();
            var steps = new List<long>();

            foreach (var node in endingANodes)
            {
                var currentKey = node;
                var count = 0;

                while (!currentKey.EndsWith("Z"))
                {
                    var instruction = instructions[count % instructions.Length];

                    switch (instruction)
                    {
                        case 'L':
                            currentKey = map[currentKey].L;
                            count++;
                            break;
                        case 'R':
                            currentKey = map[currentKey].R;
                            count++;
                            break;
                        default:
                            throw new InvalidOperationException("Invalid instruction");
                    }
                }
                
                steps.Add(count);

            }
            
            return CalculateLCM(steps);
        }

        private long CalculateLCM(IEnumerable<long> numbers)
        {
            return numbers.Aggregate((long)1, (current, number) => current / CalculateGCD(current, number) * number);
        }

        private long CalculateGCD(long a, long b)
        {
            while (b != 0)
            {
                a %= b;
                (a, b) = (b, a);
            }
            
            return a;
        }
    }
}