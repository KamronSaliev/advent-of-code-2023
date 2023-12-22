using AdventOfCode2023.Problems.Common;

namespace AdventOfCode2023.Problems.Day1
{
    public class Day1_Trebuchet
    {
        private const string InputPath = "../../../Problems/Day1/Day1_Trebuchet_Input.txt";

        public void Solve()
        {
            var lines = FileOperations.ReadLines(InputPath);
            var calibrationValues = CalculateCalibrationValues(lines);
            var result = calibrationValues.Sum();
            Console.WriteLine($"Sum: {result}");
        }

        private List<int> CalculateCalibrationValues(IEnumerable<string> lines)
        {
            return lines.Select(ProcessLine).ToList();
        }

        private int ProcessLine(string line)
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