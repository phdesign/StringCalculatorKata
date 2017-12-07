using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalc
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new[] {",", "\n"};
            if (TryParseDelimiter(numbers, out var delimiter, out var numbersWithoutDelimiter))
            {
                delimiters = new[] {delimiter};
                numbers = numbersWithoutDelimiter;
            }
            
            var numbersSplit = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var numbersParsed = numbersSplit.Select(x => int.TryParse(x, out var result) ? result : 0);
            ThrowExceptionIfNegativeNumbers(numbersParsed);
            numbersParsed = numbersParsed.Where(x => x <= 1000);
            
            return numbersParsed.Sum();
        }

        private void ThrowExceptionIfNegativeNumbers(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(x => x < 0);
            if (negativeNumbers.Any())
            {
                var message = "Negatives not allowed: " + string.Join(",", negativeNumbers.Select(x => x.ToString()));
                throw new ArgumentException(message, nameof(numbers));
            }
        }

        private bool TryParseDelimiter(string input, out string delimiter, out string numbers)
        {
            delimiter = null;
            numbers = input;
            var delimiterPattern = new Regex("^//(.)\\n(.*)$");
            var delimiterMatch = delimiterPattern.Match(numbers);
            if (!delimiterMatch.Success) return false;
                
            if (delimiterMatch.Groups.Count != 3)
                throw new ArgumentException("Invalid delimiter format", nameof(numbers));
            delimiter = delimiterMatch.Groups[1].Value;
            numbers = delimiterMatch.Groups[2].Value;
            return true;
        }
    }
}