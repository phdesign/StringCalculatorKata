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

        private void AddTest(string input, int expected)
        {
            var target = new Calculator();
            var actual = target.Add(input);
            Assert.Equal(expected, actual);
        }
    }
}