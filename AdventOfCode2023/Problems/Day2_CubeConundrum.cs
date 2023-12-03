using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Problems
{
    public class Day2_CubeConundrum
    {
        private readonly string _inputPath;

        private const int RedCubeCount = 12;
        private const int GreenCubeCount = 13;
        private const int BlueCubeCount = 14;

        public Day2_CubeConundrum(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(_inputPath);
            var possibleGameIDs = GetInfo(lines);
            var result = possibleGameIDs.Sum();
            Console.WriteLine($"Sum: {result}");
        }

        private List<int> GetInfo(IEnumerable<string> lines)
        {
            return lines.Select(GetLineInfo).ToList();
        }

        private int GetLineInfo(string line)
        {
            var gameInfo = line.Split(':');
            var gameID = int.Parse(gameInfo.First().Replace("Game","").Trim());
            var gameDescription = gameInfo.Last().Trim();
            var rounds = gameDescription.Split(';');

            foreach (var round in rounds)
            {
                var cubes = round.Trim().Split(',');
                var (red, green, blue) = GetCubeValues(cubes);
                
                if (red > RedCubeCount || green > GreenCubeCount || blue > BlueCubeCount)
                {
                    return 0;
                }
            }
            
            return gameID;
        }

        private (int Red, int Green, int Blue) GetCubeValues(string[] cubes)
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