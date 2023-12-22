using System.Text.RegularExpressions;
using AdventOfCode2023.Problems.Common;

namespace AdventOfCode2023.Problems.Day8
{
    public class Day8_HauntedWasteland
    {
        private const string InputPath = "../../../Problems/Day8/Day8_HauntedWasteland_Input.txt";
        
        private const int InstructionsLineIndex = 0;
        private const int MapStartLineIndex = 2;

        private enum Instruction
        {
            Left = 'L',
            Right = 'R'
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(InputPath);
            var instructions = ParseInstructions(lines[InstructionsLineIndex]);
            var map = ParseMap(lines.Skip(MapStartLineIndex));

            var result1 = CountStepsToReachEndingNodes(instructions, map, "AAA", "ZZZ");
            Console.WriteLine($"Steps to ZZZ node: {result1}");

            var result2 = CountStepsToReachAllEndingNodes(instructions, map, "A", "Z");
            Console.WriteLine($"Steps to all Z nodes: {result2}");
        }

        private List<Instruction> ParseInstructions(string line)
        {
            return line.Select(c => (Instruction)c).ToList();
        }

        private Dictionary<string, (string L, string R)> ParseMap(IEnumerable<string> lines)
        {
            var map = new Dictionary<string, (string, string)>();

            foreach (var line in lines.Where(line => !string.IsNullOrEmpty(line)))
            {
                var parsedData = Regex.Matches(line, "\\w+");
                map.Add(parsedData[0].Value, (parsedData[1].Value, parsedData[2].Value));
            }

            return map;
        }

        private long CountStepsToReachEndingNodes(IEnumerable<Instruction> instructions,
            Dictionary<string, (string L, string R)> map, string current, string target)
        {
            return CountStepsToNode(instructions, map, current, target);
        }

        private long CountStepsToReachAllEndingNodes(IEnumerable<Instruction> instructions,
            Dictionary<string, (string L, string R)> map, string current, string target)
        {
            var startNodes = map.Keys.Where(key => key.EndsWith(current));
            var steps = startNodes.Select(node => CountStepsToNode(instructions, map, node, target));
            return CalculateLCM(steps);
        }

        private long CountStepsToNode(IEnumerable<Instruction> instructions,
            Dictionary<string, (string L, string R)> map, string current, string target)
        {
            var instructionArray = instructions.ToArray();
            var currentKey = current;
            var count = 0L;

            while (!currentKey.EndsWith(target))
            {
                var instruction = instructionArray[count % instructionArray.Length];
                currentKey = instruction == Instruction.Left ? map[currentKey].L : map[currentKey].R;
                count++;
            }

            return count;
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