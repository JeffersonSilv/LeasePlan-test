using LeasePlanCurrencyConverter.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeasePlanCurrencyConverter.Adapter
{
    public class ExchangeRateAdapter : IExchangeRateAdapter
    {
        private readonly HttpClient _client;
        private readonly ExternalApiSettings _externalApiSettings;
        public ExchangeRateAdapter(HttpClient client, IOptions<ExternalApiSettings> externalApiSettings)
        {
            _client = client;
            _externalApiSettings = externalApiSettings.Value;
        }

        public async Task<double> GetExchangeRateAsync(string currencyCodeFrom, string currencyCodeTo)
        {
            var parameters = GetUriParameters(currencyCodeFrom, currencyCodeTo);
            var response = await _client.GetAsync(parameters);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error when calling Api to get exchange rate - code {response.StatusCode}");
            }

            var result = await response.Content.ReadAsAsync<JObject>();
            if (!result.HasValues)
            {
                throw new Exception($"Error when calling Api to get exchange rate - returned no value");
            }
            
            return (double)result.First;
        }

        private string GetUriParameters(string currencyCodeFrom, string currencyCodeTo)
        {
            return $"?q={currencyCodeFrom}_{currencyCodeTo}&compact=ultra&apiKey={_externalApiSettings.ApiKey}";
        }
    }
}
