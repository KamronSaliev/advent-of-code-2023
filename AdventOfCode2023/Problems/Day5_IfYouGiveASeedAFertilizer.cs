using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Problems
{
    public class Day5_IfYouGiveASeedAFertilizer
    {
        private struct MapData
        {
            public readonly long DestinationIndex;
            public readonly long SourceIndex;
            public readonly long Length;

            public MapData(long destinationIndex, long sourceIndex, long length)
            {
                DestinationIndex = destinationIndex;
                SourceIndex = sourceIndex;
                Length = length;
            }

            public override string ToString()
            {
                return $"{SourceIndex} {DestinationIndex} {Length}";
            }
        }

        private const int SeedsLineIndex = 0;
        private const int MapsStartLineIndex = 3;
        private const int MapLineIndexIncrement = 2;
        
        private readonly string _inputPath;

        public Day5_IfYouGiveASeedAFertilizer(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(_inputPath);
            
            var seeds = ProcessNumbers(SeedsLineIndex, lines);
            var lineIndex = MapsStartLineIndex;
            var seedToSoilMap = ProcessMap(ref lineIndex, lines);
            var soilToFertilizerMap = ProcessMap(ref lineIndex, lines);
            var fertilizerToWater = ProcessMap(ref lineIndex, lines);
            var waterToLight = ProcessMap(ref lineIndex, lines);
            var lightToTemperature = ProcessMap(ref lineIndex, lines);
            var temperatureToHumidity = ProcessMap(ref lineIndex, lines);
            var humidityToLocation = ProcessMap(ref lineIndex, lines);
            
            var result = long.MaxValue;
            
            foreach (var seed in seeds)
            {
                var value = GetMapValue(seed, seedToSoilMap);
                value = GetMapValue(value, soilToFertilizerMap);
                value = GetMapValue(value, fertilizerToWater);
                value = GetMapValue(value, waterToLight);
                value = GetMapValue(value, lightToTemperature);
                value = GetMapValue(value, temperatureToHumidity);
                value = GetMapValue(value, humidityToLocation);
                result = Math.Min(result, value);
            }

            Console.WriteLine($"Lowest Location: {result}");
        }

        private long GetMapValue(long number, List<MapData> map)
        {
            foreach (var mapData in map)
            {
                if (number >= mapData.SourceIndex && 
                    number <= mapData.SourceIndex + mapData.Length - 1)
                {
                    return number - mapData.SourceIndex + mapData.DestinationIndex;
                }
            }
            
            return number;
        }

        private long[] ProcessNumbers(int lineIndex, List<string> lines)
        {
            var numbers = Array.ConvertAll
            (
                lines[lineIndex].Split(':').Last().Trim().Split(' '),
                long.Parse
            );
            
            return numbers;
        }

        private List<MapData> ProcessMap(ref int lineIndex, List<string> lines)
        {
            var map = new List<MapData>();

            while (lineIndex < lines.Count && lines[lineIndex] != string.Empty)
            {
                var line = lines[lineIndex];
                var mapData = ProcessMapData(line);
                map.Add(mapData);
                
                lineIndex++;
            }

            lineIndex += MapLineIndexIncrement;

            return map;
        }

        private MapData ProcessMapData(string line)
        {
            var data = Array.ConvertAll(line.Split(' '), long.Parse);
            return new MapData(data[0], data[1], data[2]);
        }
    }
}