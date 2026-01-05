using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using Moq;
using System;
using Xunit;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Tests
{

    public class CurrencyConverterTests
    {
        [Fact(DisplayName = "Converting result with a valid rate multiplies amount with rate")]
        public void Convert_RateGreaterThenZero_ReturnsProductOfAmountAndRate() //testing Convert(decimal, decimal)
        {
            //arrange
            decimal amount = 2.5M;
            decimal rate = 2.0M;
            decimal expectedResult = 5.0M;

            var rateService = new Mock<IRateService>();
            var converter = new CurrencyConverter(rateService.Object);

            //act
            decimal result = converter.Convert(amount, rate);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "Converting with a rate <= 0 throws Exception", Skip = "Skipped: already tested in Convert_RateLessOrEqualToZero_ThrowsArgumentExceptionWithParamNameRate")]
        [InlineData(0)]
        [InlineData(-12.345)]
        public void Convert_RateLessOrEqualToZero_ThrowsException(decimal rate) //testing Convert(decimal, decimal)
        {
            //arrange
            decimal amount = 2.5M;

            var rateService = new Mock<IRateService>();
            var converter = new CurrencyConverter(rateService.Object);

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate));

            //assert
            Assert.ThrowsAny<Exception>(conversion);
        }

        [Theory(DisplayName = "Converting with a rate <= 0 throws ArgumentException containing the parameter name")]
        [InlineData(0)]
        [InlineData(-12.345)]
        public void Convert_RateLessOrEqualToZero_ThrowsArgumentExceptionWithParamNameRate(decimal rate) //testing Convert(decimal, decimal)
        {
            //arrange
            decimal amount = 2.5M;
            var expectedParamName = "rate";

            var rateService = new Mock<IRateService>();
            var converter = new CurrencyConverter(rateService.Object);

            //act
            Action conversion = new Action(() => converter.Convert(amount, rate));

            //assert
            var exception = Assert.Throws<ArgumentException>(conversion);
            Assert.Equal(expectedParamName, exception.ParamName);
        }

        [Fact(DisplayName = "Converting between known currencies returns correct foreign amount")]
        public void Convert_ConvertibleCurrencies_ReturnsProductOfAmountAndRate() //testing Convert(decimal, string, string)
        {
            //arrange
            decimal amount = 2.5M;

            var rate = new Rate
            {
                FromCurrency = "AAA",
                ToCurrency = "BBB",
                ExchangeRate = 2.0M
            };

            decimal expectedResult = 5.0M;

            var rateService = new Mock<IRateService>();
            rateService
                .Setup(m => m.CanConvertBetween(rate.FromCurrency, rate.ToCurrency))
                .Returns(true);

            rateService
                .Setup(m => m.GetRate(rate.FromCurrency, rate.ToCurrency))
                .Returns(rate);

            var converter = new CurrencyConverter(rateService.Object);

            //act
            decimal result = converter.Convert(amount, rate.FromCurrency, rate.ToCurrency);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact(DisplayName = "Currency Conversion uses IRateService.GetRate")]
        public void Convert_CallsIRateServiceGetRate() //testing Convert(decimal, string, string)
        {
            //arrange
            decimal amount = 2.5M;
            var rate = new Rate
            {
                FromCurrency = "AAA",
                ToCurrency = "BBB",
                ExchangeRate = 2.0M
            };

            var rateService = new Mock<IRateService>();
            rateService
                .Setup(m => m.CanConvertBetween(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            rateService
               .Setup(m => m.GetRate(It.IsAny<string>(), It.IsAny<string>()))
               .Returns(rate);

            var converter = new CurrencyConverter(rateService.Object);

            //act
            converter.Convert(amount, rate.FromCurrency, rate.ToCurrency);

            //assert
            rateService.Verify(mock => mock.GetRate(rate.FromCurrency, rate.ToCurrency));
        }

        [Fact(DisplayName = "Converting to or from an unknown currency throws NotSupportedException")]
        public void Convert_InconvertibleCurrencies_NotSupportedException() //testing Convert(decimal, string, string)
        {
            //arrange
            decimal amount = 2.5M;
            string fromCurrency = "AAA";
            string toCurrency = "BBB";

            var rateService = new Mock<IRateService>();
            rateService
                .Setup(m => m.CanConvertBetween(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var converter = new CurrencyConverter(rateService.Object);

            //act
            Action conversion = new Action(() => converter.Convert(amount, fromCurrency, toCurrency));

            //assert
            Assert.Throws<NotSupportedException>(conversion);
        }

    }
}
