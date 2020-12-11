using System.Collections.Generic;
using System.Threading.Tasks;
using LeasePlanCurrencyConverter.Domain;

namespace LeasePlanCurrencyConverter.Data
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetCurrenciesAsync();
    }
}