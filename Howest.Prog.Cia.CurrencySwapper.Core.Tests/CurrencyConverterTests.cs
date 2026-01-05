using System;
using Xunit;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Tests
{

    public class CurrencyConverterTests
    {
        [Fact]
        public void Convert_RateGreaterThenZero_ReturnsProductOfAmountAndRate()
        {
            //arrange
            var converter = new CurrencyConverter();
            decimal amount = 2.5M;
            decimal rate = 2.0M;
            decimal expectedResult = 5.0M;

            //act
            decimal result = converter.Convert(amount, rate);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-12.345)]
        public void Convert_RateLessOrEqualToZero_ThrowsException(decimal rate)
        {
            //arrange
            var converter = new CurrencyConverter();
            decimal amount = 2.5M;

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate));

            //assert
            Assert.ThrowsAny<Exception>(conversion);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-12.345)]
        public void Convert_RateLessOrEqualToZero_ThrowsArgumentExceptionWithParamNameRate(decimal rate)
        {
            //arrange
            var converter = new CurrencyConverter();
            decimal amount = 2.5M;
            string expectedParamName = "rate";

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate));

            //assert
            var exception = Assert.Throws<ArgumentException>(conversion);
            Assert.Equal(expectedParamName, exception.ParamName);
        }
    }
}
