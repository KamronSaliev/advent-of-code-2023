using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2023.Common;

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

            var result1 = CalculateTotalWinnings(lines, false);
            Console.WriteLine($"Total winnings: {result1}");

            var result2 = CalculateTotalWinnings(lines, true);
            Console.WriteLine($"Total winnings with Jokers: {result2}");
        }

        private int CalculateTotalWinnings(List<string> lines, bool isWithJokers)
        {
            var hands = GetHands(lines, isWithJokers);
            var handComparer = new HandComparer();
            hands.Sort(handComparer);
            return hands.Select((hand, index) => hand.Bid * (index + 1)).Sum();
        }

        private List<Hand> GetHands(List<string> lines, bool isWithJokers)
        {
            return lines.Select(line => ProcessLine(line, isWithJokers)).ToList();
        }

        private Hand ProcessLine(string line, bool isWithJokers)
        {
            var parts = line.Split(' ');
            var description = parts[0];
            var bid = int.Parse(parts[1]);
            return new Hand(description, bid, isWithJokers);
        }
    }
}