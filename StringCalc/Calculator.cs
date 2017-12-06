using System;
using System.Linq;

namespace StringCalc
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new[] {",", "\n"};
            if (numbers.StartsWith("//"))
            {
                var delimiterSplit = numbers.Substring(2).Split("\n");
                if (delimiterSplit.Length <= 1) 
                    throw new ArgumentException("Invalid format", nameof(numbers));
                delimiters = delimiterSplit.Take(1).ToArray();
                numbers = numbers.Substring(2 + delimiters[0].Length);
            }
            
            var numbersSplit = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var numbersParsed = numbersSplit.Select(x => int.TryParse(x, out var result) ? result : 0);
            return numbersParsed.Sum();
        }
    }
}