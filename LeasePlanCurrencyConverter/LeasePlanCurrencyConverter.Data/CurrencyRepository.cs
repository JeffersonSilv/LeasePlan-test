using LeasePlanCurrencyConverter.Common;
using LeasePlanCurrencyConverter.Domain;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LeasePlanCurrencyConverter.Data
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly Settings _settings;
        public CurrencyRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            var json = File.OpenRead(_settings.GetCurrenciesFullPath());

            return await JsonSerializer.DeserializeAsync<IEnumerable<Currency>>(json);
        }
    }
}
