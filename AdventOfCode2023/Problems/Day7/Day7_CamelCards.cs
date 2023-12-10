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
            var hands = GetHands(lines, false);
            var handsWithJokers = GetHands(lines, true);
            var handComparer = new HandComparer();
            hands.Sort(handComparer);
            handsWithJokers.Sort(handComparer);

            var result1 = hands.Select((hand, i) => hand.Bid * (i + 1)).Sum();
            Console.WriteLine($"Total winnings: {result1}");

            var result2 = handsWithJokers.Select((hand, i) => hand.Bid * (i + 1)).Sum();
            Console.WriteLine($"Total winnings with Jokers: {result2}");
        }

        private List<Hand> GetHands(List<string> lines, bool isWithJokers)
        {
            return lines.Select(line => ProcessLine(line, isWithJokers)).ToList();
        }

        private Hand ProcessLine(string line, bool isWithJokers)
        {
            var description = line.Split(' ').First();
            var bid = int.Parse(line.Split(' ').Last());
            return new Hand(description, bid, isWithJokers);
        }
    }
}