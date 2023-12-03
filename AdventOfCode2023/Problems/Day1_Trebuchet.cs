using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Problems
{
    public class Day1_Trebuchet
    {
        private readonly string _inputPath;

        public Day1_Trebuchet(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(_inputPath);
            var calibrationValues = ParseCalibrationValues(lines);

            var result = calibrationValues.Sum();

            Console.WriteLine($"Sum: {result}");
        }

        private List<int> ParseCalibrationValues(IEnumerable<string> lines)
        {
            return lines.Select(ParseLine).ToList();
        }

        private int ParseLine(string line)
        {
            var numberWords = new Dictionary<string, int>
            {
                { "one", 1 }, { "two", 2 }, { "three", 3 },
                { "four", 4 }, { "five", 5 }, { "six", 6 },
                { "seven", 7 }, { "eight", 8 }, { "nine", 9 }
            };

            var digits = new List<int>();

            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    digits.Add(line[i] - '0');
                }
                else
                {
                    foreach (var numberWord in numberWords)
                    {
                        if (i + numberWord.Key.Length > line.Length)
                        {
                            continue;
                        }

                        var lineSubString = line.Substring(i, numberWord.Key.Length);
                        
                        if (string.CompareOrdinal(lineSubString, numberWord.Key) == 0)
                        {
                            digits.Add(numberWord.Value);
                        }
                    }
                }
            }

            Console.WriteLine($"{line} -> {string.Join(" ", digits)} -> {digits.First() * 10 + digits.Last()}");

            return digits.First() * 10 + digits.Last();
        }
    }
}