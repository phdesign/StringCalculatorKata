using System;
using Xunit;

namespace StringCalc.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("12", 12)]
        [InlineData("1,2", 3)]
        [InlineData("0,2", 2)]
        [InlineData("12,12", 24)]
        public void Add_ShouldCalculateSum_GivenZeroOneOrTwoNumbers(string input, int expected)
        {
            AddTest(input, expected);
        }
        
        [Theory]
        [InlineData("1,1,1", 3)]
        [InlineData("1,1,1,1", 4)]
        [InlineData("9,9,9,9,9", 45)]
        public void Add_ShouldCalculateSum_GivenAnyAmountOfNumbers(string input, int expected)
        {
            AddTest(input, expected);
        }
        
        [Theory]
        [InlineData("1\n2", 3)]
        [InlineData("1\n2,3", 6)]
        public void Add_ShouldCalculateSum_GivenNumbersSeparatedByNewline(string input, int expected)
        {
            AddTest(input, expected);
        }
        
        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//\n\n1\n2", 3)]
        [InlineData("//a\n1a2a3", 6)]
        [InlineData("///\n1/2", 3)]
        public void Add_ShouldCalculateSum_GivenDelimiterPattern(string input, int expected)
        {
            AddTest(input, expected);
        }
        
        [Theory]
        [InlineData("//;\n1;-2", "Negatives not allowed: -2")]
        [InlineData("1,-2", "Negatives not allowed: -2")]
        [InlineData("-1\n-2\n3", "Negatives not allowed: -1,-2")]
        [InlineData("-1", "Negatives not allowed: -1")]
        public void Add_ShouldThrowException_GivenNegativeInputs(string input, string expectedException)
        {
            var ex = Assert.Throws<ArgumentException>(() => AddTest(input, -1));
            Assert.Equal(expectedException + "\r\nParameter name: numbers", ex.Message);
        }
        
        [Theory]
        [InlineData("1000", 1000)]
        [InlineData("1001", 0)]
        [InlineData("2,1001", 2)]
        public void Add_ShouldIgnoreNumbersBiggerThan1000(string input, int expected)
        {
            AddTest(input, expected);
        }

        private void AddTest(string input, int expected)
        {
            var target = new Calculator();
            var actual = target.Add(input);
            Assert.Equal(expected, actual);
        }
    }
}