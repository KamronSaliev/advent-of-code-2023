using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Problems.Day7
{
    public class Day7_CamelCards
    {
        private readonly string _inputPath;

        public Day7_CamelCards(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(_inputPath);
            var hands = GetHands(lines);
            hands.Sort(new HandComparer());

            var result = hands.Select((hand, i) => hand.Bid * (i + 1)).Sum();
            Console.WriteLine($"Total winnings: {result}");
        }

        private List<Hand> GetHands(IEnumerable<string> lines)
        {
            return lines.Select(ProcessLine).ToList();
        }

        private Hand ProcessLine(string line)
        {
            var description = line.Split(' ').First();
            var bid = int.Parse(line.Split(' ').Last());
            return new Hand(description, bid);
        }
    }
}