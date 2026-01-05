using System;
using System.Collections.Generic;
using System.Text;

namespace Howest.Prog.Cia.CurrencySwapper.Infrastructure.CurrConv
{
    public class RatesDto
    {
        public Dictionary<string, double> Rates { get; set; }
    }

    public class CurrrenciesDto
    {
        public Dictionary<string, string> Symbols { get; set; }
    }

}
