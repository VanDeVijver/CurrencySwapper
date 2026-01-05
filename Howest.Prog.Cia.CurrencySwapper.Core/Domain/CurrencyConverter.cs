using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using System;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Domain
{
    public class CurrencyConverter
    {
        public const string AmountMustBePositive = "Conversion rate must be greater than zero";
        public const string CantConvert = "Can't convert between {0} and {1}";
        private readonly IRateService _rateService;

        public CurrencyConverter(IRateService rateService)
        {
            _rateService = rateService;
        }

        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (_rateService.CanConvertBetween(fromCurrency, toCurrency))
            {
                Rate rate = _rateService.GetRate(fromCurrency, toCurrency);
                return Convert(amount, rate.ExchangeRate);
            }
            else
            {
                throw new NotSupportedException(string.Format(CantConvert, fromCurrency, toCurrency));
            }
        }

        public decimal Convert(decimal amount, decimal rate)
        {
            if (rate <= 0)   //domain rule, koers kan nooit negatief of nul zijn
            {
                throw new ArgumentException(AmountMustBePositive, nameof(rate));
            }
            else
            {
                return amount * rate;
            }
        }
    }
}
