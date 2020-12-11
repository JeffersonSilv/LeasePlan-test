using System.Threading.Tasks;

namespace LeasePlanCurrencyConverter.Adapter
{
    public interface IExchangeRateAdapter
    {
        Task<double> GetExchangeRateAsync(string currencyCodeFrom, string currencyCodeTo);
    }
}