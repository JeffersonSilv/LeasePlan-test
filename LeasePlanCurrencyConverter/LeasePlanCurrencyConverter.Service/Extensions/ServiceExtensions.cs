using System;
using System.Collections.Generic;
using System.Text;
using LeasePlanCurrencyConverter.Data;
using Microsoft.Extensions.DependencyInjection;

namespace LeasePlanCurrencyConverter.Service.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection WithServiceDependencies(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<ICurrencyService, CurrencyService>();
        }
    }
}
