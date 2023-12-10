using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Problems.Day2
{
    public class Day2_CubeConundrum
    {
        private readonly string _inputPath;

        public Day2_CubeConundrum(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(_inputPath);
            var possibleGameIDs = GetPossibleGameIDs(lines);
            var result = possibleGameIDs.Sum();
            Console.WriteLine($"Sum: {result}");
        }

        private List<int> GetPossibleGameIDs(IEnumerable<string> lines)
        {
            return lines.Select(ProcessLine).ToList();
        }

        private int ProcessLine(string line)
        {
            var gameInfo = line.Split(':');
            var gameDescription = gameInfo.Last().Trim();
            var rounds = gameDescription.Split(';');

            var maxRed = -1;
            var maxGreen = -1;
            var maxBlue = -1;
            
            foreach (var round in rounds)
            {
                var cubes = round.Trim().Split(',');
                var (red, green, blue) = ProcessCubeValues(cubes);

                maxRed = Math.Max(maxRed, red);
                maxGreen = Math.Max(maxGreen, green);
                maxBlue = Math.Max(maxBlue, blue);
            }
            
            Console.WriteLine($"{maxRed}*{maxGreen}*{maxBlue}={maxRed * maxGreen * maxBlue}");

            return maxRed * maxGreen * maxBlue;
        }

        private (int Red, int Green, int Blue) ProcessCubeValues(string[] cubes)
        {
            var red = 0;
            var green = 0;
            var blue = 0;
            
            foreach (var cube in cubes)
            {
                var cubeValue = cube.Trim().Split(' ').First();
                var cubeType = cube.Trim().Split(' ').Last();

                switch (cubeType)
                {
                    case "red":
                        red = int.Parse(cubeValue);
                        break;
                    case "green":
                        green = int.Parse(cubeValue);
                        break;
                    case "blue":
                        blue = int.Parse(cubeValue);
                        break;
                }
            }

            return (red, green, blue);
        }
    }
}