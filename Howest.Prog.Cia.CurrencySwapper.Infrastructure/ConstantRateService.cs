using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Howest.Prog.Cia.CurrencySwapper.Infrastructure
{
    public class ConstantRateService : IRateService
    {
        private readonly List<Rate> _rates = new List<Rate>
        {
            new Rate { ExchangeRate = 1.189421M, FromCurrency = "EUR", ToCurrency = "USD" },
            new Rate { ExchangeRate = 1 / 1.189421M, FromCurrency = "USD", ToCurrency = "EUR" },
        };

        public Rate GetRate(string fromCurrency, string toCurrency)
        {
            return _rates.SingleOrDefault(r => 
                r.FromCurrency == fromCurrency && 
                r.ToCurrency == toCurrency
            );
        }

        public IEnumerable<string> GetSupportedCurrencies()
        {
            return _rates.Select(e => e.FromCurrency).Distinct().OrderBy(currency => currency);
        }

        public bool CanConvertBetween(string fromCurrency, string toCurrency)
        {
            var currencies = GetSupportedCurrencies();
            return currencies.Any(c => c == fromCurrency || c == toCurrency);
        }
    }
}
