using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using Howest.Prog.Cia.CurrencySwapper.Infrastructure.CurrConv;
using Howest.Prog.Cia.CurrencySwapper.Infrastructure.Realtime.KeyConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Howest.Prog.Cia.CurrencySwapper.Infrastructure.Realtime
{
    /// <summary>
    /// Queries the online ExchangeRatesApi which provides currency rates published by the European Central Bank
    /// </summary>
    /// <remarks>
    /// View documentation here https://apilayer.com/marketplace/exchangerates_data-api
    /// API key should be provided (appsettings.json, Visual Studio user secrets, ...)
    /// Create API key through: https://apilayer.com/marketplace/exchangerates_data-api?preview=true#pricing
    /// Using Visual Studio user secrets:
    /// Right click project Howest.Prog.Cia.CurrencySwapper.Infrastructure, Manage User Secrets
    /// Provide:
    /// {
    ///    "Howest.Prog.Cia.CurrencySwapper.Infrastructure.Realtime.KeyConfig.ConfigurationOptions": {
    ///       "ApiLayerApiKey": "YOUR KEY HERE"
    ///    }
    /// }
    /// </remarks>
    public class ExchangeRatesApiClient : IRateService
    {
        private string ApiCurrenciesUrl = "https://api.apilayer.com/exchangerates_data/symbols";
        private string ApiRatesUrl = "https://api.apilayer.com/exchangerates_data/latest?symbols={0}&base={1}";

        public ExchangeRatesApiClient()
        {

        }
        
        public Rate GetRate(string fromCurrency, string toCurrency)
        {
            decimal rate = 0M;
            string url = string.Format(ApiRatesUrl, toCurrency, fromCurrency);
            string json = ReadJsonFromUrl(url);

            using (var document = JsonDocument.Parse(json))
            {
                rate = document.RootElement.GetProperty("rates").GetProperty(toCurrency).GetDecimal();
            }

            return new Rate
            {
                ExchangeRate = rate,
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency
            };
        }

        public IEnumerable<string> GetSupportedCurrencies()
        {
            string url = string.Format(ApiCurrenciesUrl);
            string json = ReadJsonFromUrl(url);
            var dto = JsonSerializer.Deserialize<CurrrenciesDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var supportedCurrencies = dto.Symbols.Select(result => result.Key).ToList();
            return supportedCurrencies.OrderBy(code => code);
        }

        public bool CanConvertBetween(string fromCurrency, string toCurrency)
        {
            var currencies = GetSupportedCurrencies();
            return currencies.Any(c => c == fromCurrency || c == toCurrency);
        }

        protected string ReadJsonFromUrl(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddHeader("apikey", OptainApiKey());
            RestResponse response = client.Execute(request);
            return response.Content;    
        }

        protected string OptainApiKey()
        {
            var services = ServiceProviderBuilder.GetServiceProvider(new string[] { });
            var options = services.GetRequiredService<IOptions<ConfigurationOptions>>();
            return options.Value.ApiLayerApiKey;
        }
    }
}
