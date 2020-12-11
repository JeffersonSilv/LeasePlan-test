using Microsoft.Extensions.DependencyInjection;

namespace LeasePlanCurrencyConverter.Data.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection WithDataDependencies(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<ICurrencyRepository, CurrencyRepository>();
        }
    }
}
