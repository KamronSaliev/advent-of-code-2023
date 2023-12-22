using AdventOfCode2023.Problems.Common;

namespace AdventOfCode2023.Problems.Day9
{
    public class Day9_MirageMaintenance
    {
        private const string InputPath = "../../../Problems/Day9/Day9_MirageMaintenance_Input.txt";

        public void Solve()
        {
            var lines = FileOperations.ReadLines(InputPath);
            var numbersList = ProcessLines(lines);
            var result = 0;

            foreach (var numbers in numbersList)
            {
                var differences = new List<List<int>> { numbers };
                Console.WriteLine(string.Join(" ", numbers));
                
                var currentNumbers = numbers;
                while (currentNumbers.Exists(x => x != 0))
                {
                    currentNumbers = new List<int>();

                    for (var i = 0; i < differences[^1].Count - 1; i++)
                    {
                        var diff = differences[^1][i + 1] - differences[^1][i];
                        currentNumbers.Add(diff);
                    }

                    differences.Add(currentNumbers);
                }
            }
        }
        
        private List<List<int>> ProcessLines(List<string> lines)
        {
            return lines.Select(ProcessLine).ToList();
        }

        private List<int> ProcessLine(string line)
        {
            return Array.ConvertAll(line.Split(' '), int.Parse).ToList();
        }
    }
}