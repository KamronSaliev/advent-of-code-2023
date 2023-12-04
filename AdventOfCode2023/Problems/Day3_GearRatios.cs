using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problems
{
    public class Day3_GearRatios
    {
        private readonly string _inputPath;
        
        private readonly (int X, int Y)[] _directions =
        {
            (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1)
        };

        private readonly char[] _matchableSymbols =
        {
            '+', '-', '*', '/', '@', '#', '$', '%', '&', '='
        };

        private readonly Dictionary<(int X, int Y), List<int>> _gears = new Dictionary<(int X, int Y), List<int>>();

        public Day3_GearRatios(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(_inputPath);
            var result = 0;
            var resultGearRatio = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                var matches = Regex.Matches(lines[i], "\\d+");

                for (var j = 0; j < matches.Count; j++)
                {
                    result += ProcessMatch(matches[j], lines, i);
                }
            }

            foreach (var gear in _gears)
            {
                Console.WriteLine($"{lines[gear.Key.X][gear.Key.Y]} {string.Join(" ", gear.Value)}");
                
                if (gear.Value.Count != 2)
                {
                    continue;
                }

                var product = 1;
                foreach (var number in gear.Value)
                {
                    product *= number;
                }

                resultGearRatio += product;
            }

            Console.WriteLine($"Sum: {result}");
            Console.WriteLine($"Gear Ratio: {resultGearRatio}");
        }

        private int ProcessMatch(Match match, List<string> lines, int row)
        {
            var isSymbolAdjacent = false;
            var matchValue = int.Parse(match.Value);

            for (var i = match.Index; i < match.Index + match.Length; i++)
            {
                foreach (var direction in _directions)
                {
                    var x = row + direction.X;
                    var y = i + direction.Y;

                    if (x < 0 || x >= lines.Count || y < 0 || y >= lines[0].Length)
                    {
                        continue;
                    }

                    if (_matchableSymbols.All(c => lines[x][y] != c) || isSymbolAdjacent)
                    {
                        continue;
                    }

                    isSymbolAdjacent = true;

                    if (lines[x][y] != '*')
                    {
                        continue;
                    }

                    if (!_gears.TryGetValue((x, y), out var numbers))
                    {
                        _gears[(x, y)] = new List<int> { matchValue };
                    }
                    else
                    {
                        numbers.Add(matchValue);
                    }
                }
            }

            Console.WriteLine($"{match.Value}, isSymbolAdjacent: {isSymbolAdjacent}");

            return !isSymbolAdjacent ? 0 : matchValue;
        }
    }
}