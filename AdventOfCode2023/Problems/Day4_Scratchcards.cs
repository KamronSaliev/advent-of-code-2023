using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2023.Problems
{
    public class Day4_Scratchcards
    {
        private readonly string _inputPath;

        public Day4_Scratchcards(string inputPath)
        {
            _inputPath = inputPath;
        }

        public void Solve()
        {
            var lines = FileOperations.ReadLines(_inputPath);
            var result = CalculateSum(lines);

            Console.WriteLine($"Sum: {result}");
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

        private int ProcessLine(string line)
        {
            var cardInfo = line.Split(':');
            var cardDescription = cardInfo.Last();
            var cardNumber = cardDescription.Split('|');
            var winningNumbersString = cardNumber.First().Trim();
            var winningNumbers = ProcessNumbers(winningNumbersString);
            var userNumbersString = cardNumber.Last().Trim();
            var userNumbers = ProcessNumbers(userNumbersString);
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