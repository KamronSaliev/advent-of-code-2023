using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Problems.Day7
{
    public class Hand
    {
        public readonly HandType Type;
        public readonly int Bid;
        public readonly List<int> Cards = new();

        private readonly string _description;

        public Hand(string description, int bid, bool isWithJokers = false)
        {
            _description = description;

            Bid = bid;

            foreach (var card in description)
            {
                switch (card)
                {
                    case 'A':
                        Cards.Add(14);
                        break;
                    case 'K':
                        Cards.Add(13);
                        break;
                    case 'Q':
                        Cards.Add(12);
                        break;
                    case 'J':
                        Cards.Add(isWithJokers ? 1 : 11);
                        break;
                    case 'T':
                        Cards.Add(10);
                        break;
                    default:
                        Cards.Add(int.Parse(card.ToString()));
                        break;
                }
            }

            var cardCount = CountCards(Cards);
            Type = isWithJokers ? CalculateHandTypeWithJokers(cardCount) : CalculateHandType(cardCount);
        }

        private Dictionary<int, int> CountCards(List<int> cards)
        {
            var cardCount = new Dictionary<int, int>();

            foreach (var card in cards)
            {
                if (cardCount.TryGetValue(card, out var value))
                {
                    cardCount[card] = value + 1;
                }
                else
                {
                    cardCount.Add(card, 1);
                }
            }

            return cardCount;
        }

        private HandType CalculateHandTypeWithJokers(Dictionary<int, int> cardCount)
        {
            var handType = CalculateHandType(cardCount);

            const int jokerValue = 1;

            if (!cardCount.TryGetValue(jokerValue, out var jokerCount))
            {
                return handType;
            }

            handType = handType switch
            {
                HandType.FourOfAKind => HandType.FiveOfAKind,
                HandType.FullHouse when jokerCount == 1 => HandType.FourOfAKind,
                HandType.FullHouse => HandType.FiveOfAKind,
                HandType.ThreeOfAKind => HandType.FourOfAKind,
                HandType.TwoPair when jokerCount == 1 => HandType.FullHouse,
                HandType.TwoPair => HandType.FourOfAKind,
                HandType.OnePair => HandType.ThreeOfAKind,
                HandType.HighCard => HandType.OnePair,
                _ => handType
            };

            return handType;
        }

        private HandType CalculateHandType(Dictionary<int, int> cardCount)
        {
            if (cardCount.Values.Any(i => i == 5))
            {
                return HandType.FiveOfAKind;
            }

            if (cardCount.Values.Any(i => i == 4))
            {
                return HandType.FourOfAKind;
            }

            if (cardCount.Values.Any(i => i == 3) && cardCount.Values.Any(i => i == 2))
            {
                return HandType.FullHouse;
            }

            if (cardCount.Values.Any(i => i == 3))
            {
                return HandType.ThreeOfAKind;
            }

            if (cardCount.Values.Count(i => i == 2) == 2)
            {
                return HandType.TwoPair;
            }

            return cardCount.Values.Any(i => i == 2) ? HandType.OnePair : HandType.HighCard;
        }

        public override string ToString()
        {
            return _description;
        }
    }
}