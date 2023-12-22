namespace AdventOfCode2023.Problems.Day7
{
    public class Hand
    {
        public HandType Type { get; private set; }
        public int Bid { get; private set; }
        public List<int> Cards { get; } = new();
        public string Description { get; }

        public Hand(string description, int bid, bool isWithJokers = false)
        {
            Description = description;
            Bid = bid;
            ProcessCards(description, isWithJokers);

            var cardCount = CountCards();
            Type = isWithJokers ? CalculateHandTypeWithJokers(cardCount) : CalculateHandType(cardCount);
        }

        private void ProcessCards(string description, bool isWithJokers)
        {
            foreach (var card in description)
            {
                Cards.Add(ProcessCard(card, isWithJokers));
            }
        }

        private int ProcessCard(char card, bool isWithJokers)
        {
            return card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => isWithJokers ? 1 : 11,
                'T' => 10,
                _ => int.TryParse(card.ToString(), out var value)
                    ? value
                    : throw new ArgumentException("Invalid card value.")
            };
        }

        private Dictionary<int, int> CountCards()
        {
            var cardCount = new Dictionary<int, int>();

            foreach (var card in Cards)
            {
                cardCount.TryGetValue(card, out var count);
                cardCount[card] = count + 1;
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
            return Description;
        }
    }
}