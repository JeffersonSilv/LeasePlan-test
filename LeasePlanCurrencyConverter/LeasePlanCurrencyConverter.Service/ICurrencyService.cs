using System.Collections.Generic;
using System.Threading.Tasks;
using LeasePlanCurrencyConverter.Domain;

namespace LeasePlanCurrencyConverter.Service
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetCurrenciesAsync();

        Task<double> GetConvertedValueAsync(string currencyCodeFrom, string currencyCodeTo, double baseAmount);
    }
}