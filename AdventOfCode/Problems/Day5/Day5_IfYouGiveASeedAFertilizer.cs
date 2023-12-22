using System.Diagnostics;
using AdventOfCode2023.Problems.Common;

namespace AdventOfCode2023.Problems.Day5
{
    public class Day5_IfYouGiveASeedAFertilizer
    {
        private const string InputPath = "../../../Problems/Day5/Day5_IfYouGiveASeedAFertilizer_Input.txt";
        
        private const int SeedsLineIndex = 0;
        private const int MapsStartLineIndex = 3;
        private const int MapLineIndexIncrement = 2;
        private const int MapsCount = 7;

        public void Solve()
        {
            var watch = Stopwatch.StartNew();

            var lines = FileOperations.ReadLines(InputPath);
            var seeds = ProcessNumbers(SeedsLineIndex, lines);
            var seedRanges = ProcessNumberRanges(SeedsLineIndex, lines);
            var maps = ProcessMaps(lines);
            var minLocationValue = GetMinLocation(seeds, maps);
            var minLocationValueInRanges = GetMinLocationInRange(seedRanges, maps);

            Console.WriteLine($"Lowest Location: {minLocationValue}");
            Console.WriteLine($"Lowest Location in Ranges: {minLocationValueInRanges}");

            watch.Stop();
            Console.WriteLine($"Stopwatch: {watch.ElapsedMilliseconds}ms, {watch.ElapsedMilliseconds / 1000.0d}s");
        }

        private List<List<MapData>> ProcessMaps(List<string> lines)
        {
            var lineIndex = MapsStartLineIndex;
            var maps = new List<List<MapData>>();

            for (var i = 0; i < MapsCount; i++)
            {
                maps.Add(ProcessMap(ref lineIndex, lines));
            }

            return maps;
        }

        private long GetMinLocation(IEnumerable<long> seeds, List<List<MapData>> maps)
        {
            return seeds.AsParallel().Select(seed => GetLocation(seed, maps)).Min();
        }

        private long GetMinLocationInRange(IEnumerable<(long Start, long Length)> seedRanges, List<List<MapData>> maps)
        {
            return seedRanges.AsParallel().SelectMany(range => ParallelEnumerable.Range(0, (int)range.Length)
                .Select(offset => GetLocation(range.Start + offset, maps))).Min();
        }

        private long GetLocation(long number, List<List<MapData>> maps)
        {
            return maps.Aggregate(number, GetMappedValue);
        }

        private long GetMappedValue(long number, List<MapData> map)
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

        private (long Start, long Length)[] ProcessNumberRanges(int lineIndex, List<string> lines)
        {
            var numbers = Array.ConvertAll
            (
                lines[lineIndex].Split(':').Last().Trim().Split(' '),
                long.Parse
            );

            var numberRanges = new (long Start, long Length)[numbers.Length / 2];

            for (var i = 0; i < numberRanges.Length; i++)
            {
                numberRanges[i] = (numbers[i * 2], numbers[i * 2 + 1]);
            }

            return numberRanges;
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