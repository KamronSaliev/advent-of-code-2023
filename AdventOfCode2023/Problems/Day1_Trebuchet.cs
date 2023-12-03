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

        public void Log(List<string> lines, List<int> calibrationValues)
        {
            for (var i = 0; i < lines.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {lines[i]} = {calibrationValues[i]}");
            }
        }

        private List<int> ParseCalibrationValues(IEnumerable<string> lines)
        {
            return lines.Select(ParseLine).ToList();
        }

        private int ParseLine(string line)
        {
            var digits = line.Where(char.IsDigit).Select(c => c - '0').ToArray();
            return digits.First() * 10 + digits.Last();
        }
    }
}