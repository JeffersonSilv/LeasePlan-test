using LeasePlanCurrencyConverter.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace LeasePlanCurrencyConverter.Adapter.Extensions
{
    public static class ExchangeRateAdapterExtensions
    {
        public static void WithAdapterDependencies(this IServiceCollection services, IConfigurationSection section)
        {
            var settings = section.Get<ExternalApiSettings>();

            services.Configure<ExternalApiSettings>(section);

            services
                .AddHttpClient<IExchangeRateAdapter, ExchangeRateAdapter>(client => client.BaseAddress = new Uri(settings.BaseAddress))
                .AddPolicyHandler(GetRetryPolicy(settings)); 
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(ExternalApiSettings settings)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(settings.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
