namespace AdventOfCode2023.Problems.Day5
{
    public class MapData
    {
        public long DestinationIndex { get; }
        public long SourceIndex { get; }
        public long Length { get; }

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
}