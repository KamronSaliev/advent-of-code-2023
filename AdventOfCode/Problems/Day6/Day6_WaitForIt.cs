using System.Text.RegularExpressions;
using AdventOfCode2023.Problems.Common;

namespace AdventOfCode2023.Problems.Day6
{
    public class Day6_WaitForIt
    {
        private const string InputPath = "../../../Problems/Day6/Day6_WaitForIt_Input.txt";
        
        private const int TimeLineIndex = 0;
        private const int DistanceLineIndex = 1;

        public void Solve()
        {
            var lines = FileOperations.ReadLines(InputPath);
            var times = ProcessLine(lines[TimeLineIndex]);
            var distances = ProcessLine(lines[DistanceLineIndex]);

            var result1 = 1L;

            for (var i = 0; i < times.Count; i++)
            {
                var current = CalculateWaysToWin(times[i], distances[i]);
                result1 *= current;
            }

            Console.WriteLine($"Number of Ways to Win [Result 1]: {result1}");

            var totalTime = long.Parse(string.Join("", times));
            var totalDistance = long.Parse(string.Join("", distances));
            var result2 = CalculateWaysToWin(totalTime, totalDistance);

            Console.WriteLine($"Number of Ways to Win [Result 2]: {result2}");
        }

        private List<long> ProcessLine(string line)
        {
            var numbers = new List<long>();
            var matches = Regex.Matches(line, "\\d+");

            for (var i = 0; i < matches.Count; i++)
            {
                numbers.Add(long.Parse(matches[i].Value));
            }

            return numbers;
        }

        private long CalculateWaysToWin(long time, long distance)
        {
            var sqrtDistance = Math.Sqrt(distance);
            var approximatedSqrDiscriminant = Math.Sqrt(time - 2 * sqrtDistance) * Math.Sqrt(time + 2 * sqrtDistance);
            var x2 = Math.Floor((time + approximatedSqrDiscriminant) / 2);
            var x1 = Math.Ceiling((time - approximatedSqrDiscriminant) / 2);
            return Convert.ToInt64(x2 - x1) + 1;
        }
    }
}