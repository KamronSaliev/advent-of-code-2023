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

        public Day3_GearRatios(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(_inputPath);
            var result = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                var matches = Regex.Matches(lines[i], "\\d+");

                for (var j = 0; j < matches.Count; j++)
                {
                    result += ProcessMatch(matches[j], lines, i);
                }
            }

            Console.WriteLine($"Sum: {result}");
        }

        private int ProcessMatch(Match match, List<string> lines, int row)
        {
            var isSymbolAdjacent = false;

            for (var i = match.Index; i < match.Index + match.Length; i++)
            {
                var matchX = row;
                var matchY = i;

                foreach (var direction in _directions)
                {
                    var x = matchX + direction.X;
                    var y = matchY + direction.Y;

                    if (x < 0 || x >= lines.Count || y < 0 || y >= lines[0].Length)
                    {
                        continue;
                    }

                    if (_matchableSymbols.Any(c => lines[x][y] == c))
                    {
                        isSymbolAdjacent = true;
                    }
                }
            }

            Console.WriteLine($"isSymbolAdjacent: {match.Value} {isSymbolAdjacent}");

            return !isSymbolAdjacent ? 0 : int.Parse(match.Value);
        }
    }
}