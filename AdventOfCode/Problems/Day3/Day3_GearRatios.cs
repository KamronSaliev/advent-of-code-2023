using System.Text.RegularExpressions;
using AdventOfCode2023.Problems.Common;

namespace AdventOfCode2023.Problems.Day3
{
    public class Day3_GearRatios
    {
        private const string InputPath = "../../../Problems/Day3/Day3_GearRatios_Input.txt";
        
        private readonly Dictionary<(int X, int Y), List<int>> _gears = new();

        public void Solve()
        {
            var lines = FileOperations.ReadLines(InputPath);
            var result = CalculateSum(lines);
            var resultGearRatio = CalculateGearRatio();

            Console.WriteLine($"Sum: {result}");
            Console.WriteLine($"Gear Ratio: {resultGearRatio}");
        }

        private int CalculateSum(List<string> lines)
        {
            var sum = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                sum += ProcessLine(lines, i);
            }

            return sum;
        }
        
        private int CalculateGearRatio()
        {
            var gearRatio = 0;
            
            foreach (var gear in _gears)
            {
                if (gear.Value.Count != 2)
                {
                    continue;
                }

                gearRatio += gear.Value.Aggregate(1, (current, number) => current * number);
            }

            return gearRatio;
        }

        private int ProcessLine(List<string> lines, int lineIndex)
        {
            var matches = Regex.Matches(lines[lineIndex], "\\d+");
            var sum = 0;

            for (var i = 0; i < matches.Count; i++)
            {
                sum += ProcessMatch(matches[i], lines, lineIndex);
            }

            return sum;
        }

        private int ProcessMatch(Match match, List<string> lines, int rowIndex)
        {
            var isSymbolAdjacent = false;
            var matchValue = int.Parse(match.Value);

            for (var i = match.Index; i < match.Index + match.Length; i++)
            {
                isSymbolAdjacent = CheckForAdjacentSymbols(lines, rowIndex, i, matchValue, isSymbolAdjacent) || isSymbolAdjacent;
            }

            return isSymbolAdjacent ? matchValue : 0;
        }

        private bool CheckForAdjacentSymbols(List<string> lines, int rowIndex, int columnIndex, int matchValue, bool isSymbolAdjacent)
        {
            (int X, int Y)[] directions = { (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1) };

            foreach (var direction in directions)
            {
                var x = rowIndex + direction.X;
                var y = columnIndex + direction.Y;

                if (!IsWithinBounds(x, y, lines) || !IsMatchableSymbol(lines[x][y]) || isSymbolAdjacent)
                {
                    continue;
                }

                if (lines[x][y] == '*')
                {
                    UpdateGears(x, y, matchValue);
                }

                return true;
            }

            return false;
        }

        private void UpdateGears(int x, int y, int matchValue)
        {
            if (!_gears.TryGetValue((x, y), out var numbers))
            {
                _gears[(x, y)] = new List<int> { matchValue };
            }
            else
            {
                numbers.Add(matchValue);
            }
        }

        private bool IsWithinBounds(int x, int y, List<string> lines)
        {
            return x >= 0 && x < lines.Count && y >= 0 && y < lines[0].Length;
        }

        private bool IsMatchableSymbol(char c)
        {
            char[] matchableSymbols = { '+', '-', '*', '/', '@', '#', '$', '%', '&', '=' };
            return matchableSymbols.Contains(c);
        }
    }
}