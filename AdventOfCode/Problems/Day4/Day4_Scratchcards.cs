using System.Text;
using AdventOfCode2023.Problems.Common;

namespace AdventOfCode2023.Problems.Day4
{
    public class Day4_Scratchcards
    {
        private const string InputPath = "../../../Problems/Day4/Day4_Scratchcards_Input.txt";
        
        public void Solve()
        {
            var lines = FileOperations.ReadLines(InputPath);
            var result = CalculateSum(lines);
            var totalCardCount = CalculateTotalCardCount(lines);

            Console.WriteLine($"Sum: {result}");
            Console.WriteLine($"Total Card Count: {totalCardCount}");
        }

        private int CalculateSum(List<string> lines)
        {
            var sum = 0;

            foreach (var line in lines)
            {
                sum += ProcessLine(line);
            }

            return sum;
        }

        private int CalculateTotalCardCount(List<string> lines)
        {
            var totalCardCount = 0;
            var cardCounts = new int[lines.Count];

            for (var i = 0; i < cardCounts.Length; i++)
            {
                cardCounts[i] = 1;
            }

            for (var i = 0; i < lines.Count; i++)
            {
                var price = ProcessLine(lines[i]);
                var winningCardsCount = CalculateWinningCardsCount(price);

                for (var j = 1; j <= winningCardsCount; j++)
                {
                    if (i + j >= lines.Count)
                    {
                        break;
                    }

                    cardCounts[i + j] += cardCounts[i];
                }

                totalCardCount += cardCounts[i];
            }

            return totalCardCount;
        }

        private int ProcessLine(string line)
        {
            var cardInfo = line.Split(':');
            var cardDescription = cardInfo.Last();
            var cardNumber = cardDescription.Split('|');

            var winningNumbersString = cardNumber.First().Trim();
            var winningNumbers = ProcessNumbers(winningNumbersString);

            var userNumbersString = cardNumber.Last().Trim();
            var userNumbers = ProcessNumbers(userNumbersString);

            var price = CalculateCardPrice(winningNumbers, userNumbers);

            return price;
        }

        private int CalculateCardPrice(List<int> winningNumbers, List<int> userNumbers)
        {
            var price = 0;

            foreach (var userNumber in userNumbers)
            {
                if (!winningNumbers.Contains(userNumber))
                {
                    continue;
                }

                if (price == 0)
                {
                    price = 1;
                }
                else
                {
                    price *= 2;
                }
            }

            return price;
        }

        private int CalculateWinningCardsCount(int price)
        {
            if (price == 0)
            {
                return 0;
            }

            return (int)(Math.Log(price, 2) + 1);
        }

        private List<int> ProcessNumbers(string numbersString)
        {
            var numbers = new List<int>();
            var numberStringBuilder = new StringBuilder();

            foreach (var numberString in numbersString)
            {
                if (char.IsDigit(numberString))
                {
                    numberStringBuilder.Append(numberString);
                }
                else if (numberStringBuilder.Length != 0)
                {
                    numbers.Add(int.Parse(numberStringBuilder.ToString()));
                    numberStringBuilder.Clear();
                }
            }

            numbers.Add(int.Parse(numberStringBuilder.ToString()));
            return numbers;
        }
    }
}