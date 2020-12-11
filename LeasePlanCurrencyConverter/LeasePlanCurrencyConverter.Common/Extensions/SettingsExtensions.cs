using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeasePlanCurrencyConverter.Common.Extensions
{
    public static class SettingsExtensions
    {
        public static IServiceCollection WithSettings(this IServiceCollection services, IConfigurationSection section)
        {
            return services.Configure<Settings>(section);
        }
    }
}
