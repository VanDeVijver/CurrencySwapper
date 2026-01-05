using System;
using System.Collections.Generic;
using System.Text;

namespace Howest.Prog.Cia.CurrencySwapper.Core.Domain
{
    public class Rate
    {
        public decimal ExchangeRate { get; set; }

        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }
    }
}
