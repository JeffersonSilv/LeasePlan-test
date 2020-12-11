using LeasePlanCurrencyConverter.Adapter;
using LeasePlanCurrencyConverter.Data;
using LeasePlanCurrencyConverter.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeasePlanCurrencyConverter.Service
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IExchangeRateAdapter _adapter;

        public CurrencyService(ICurrencyRepository currencyRepository, IExchangeRateAdapter adapter)
        {
            _currencyRepository = currencyRepository;
            _adapter = adapter;
        }

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            return await _currencyRepository.GetCurrenciesAsync();
        }

        public async Task<double> GetConvertedValueAsync(string currencyCodeFrom, string currencyCodeTo, double baseAmount)
        {
            var rate = await _adapter.GetExchangeRateAsync(currencyCodeFrom, currencyCodeTo);

            return rate * baseAmount;
        }
    }
}
