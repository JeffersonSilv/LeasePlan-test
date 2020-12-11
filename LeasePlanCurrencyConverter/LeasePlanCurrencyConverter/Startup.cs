using FluentValidation.AspNetCore;
using LeasePlanCurrencyConverter.Adapter.Extensions;
using LeasePlanCurrencyConverter.Common.Extensions;
using LeasePlanCurrencyConverter.Data.Extensions;
using LeasePlanCurrencyConverter.Extensions;
using LeasePlanCurrencyConverter.Service.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LeasePlanCurrencyConverter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .WithSettings(Configuration.GetSection("Settings"))
                .WithDataDependencies()
                .WithServiceDependencies()
                .WithAdapterDependencies(Configuration.GetSection("ExternalApiSettings"));

            services
                .AddControllers()
                .AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyOrigin());

            app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
