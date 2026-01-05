using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using System.Collections.Generic;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure
{
    public interface IRateService
    {
        Rate GetRate(string fromCurrency, string toCurrency);

        IEnumerable<string> GetSupportedCurrencies();

        bool CanConvertBetween(string fromCurrency, string toCurrency);
    }
}
