using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HousingManagementSystemApi
{
    using System;
    using Gateways;
    using Helpers;
    using HousingRepairsOnline.Authentication.DependencyInjection;
    using Microsoft.Extensions.Options;
    using RestSharp;
    using Services;
    using UseCases;

    public class Startup
    {
        private const string HousingManagementSystemApiIssuerId = "Housing Management System Api";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHousingRepairsOnlineAuthentication(HousingManagementSystemApiIssuerId);

            services.AddControllers();
            services.AddTransient<IRetrieveAddressesUseCase, RetrieveAddressesUseCase>();

            // var connectionString = GetEnvironmentVariable("UNIVERSAL_HOUSING_CONNECTION_STRING");
            // services.AddTransient<IAddressesRepository, UniversalHousingAddressesRepository>(_ =>
            //     new UniversalHousingAddressesRepository(() => new SqlConnection(connectionString)));
            ConfigureOptions(services);
            services.AddTransient(sp =>
            {
                var capitaOptions = sp.GetRequiredService<IOptions<CapitaOptions>>();
                return new RestClient(capitaOptions.Value.ApiAddress);
            });
            services.AddTransient<ICapitaService, CapitaService>();

            services.AddHttpClient();
            // services.AddTransient<IAddressesGateway, AddressesDatabaseGateway>();
            // services.AddTransient<IAddressesGateway, DummyAddressesGateway>();
            services.AddTransient<IAddressesGateway, AddressesCosmosGateway>();
            // services.AddTransient<IWorkOrderGateway, DummyWorkOrderGateway>();
            services.AddTransient<IWorkOrderGateway, CapitaWorkOrderGateway>();
            services.AddTransient<ICreateWorkOrderUseCase, CreateWorkOrderUseCase>();

            // services.AddTransient<ICosmosAddressQueryHelper, CosmosAddressQueryHelper>();
            services.AddCosmosAddressContainers(RepairType.All);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HousingManagementSystemApi", Version = "v1" });
                c.AddJwtSecurityScheme();
            });

            services.AddHealthChecks();
            // .AddSqlServer(connectionString, name: "Universal Housing database");
        }

        private void ConfigureOptions(IServiceCollection services)
        {
            var drsOptionsConfiguration = Configuration.GetSection(nameof(CapitaOptions));

            var requiredCapitaOptions = new[]
            {
                nameof(CapitaOptions.ApiAddress),
                nameof(CapitaOptions.Username),
                nameof(CapitaOptions.Password),
                nameof(CapitaOptions.Source),
                nameof(CapitaOptions.StandardJobCode),
                nameof(CapitaOptions.Sublocation),
            };

            foreach (var requiredCapitaOption in requiredCapitaOptions)
            {
                if (string.IsNullOrEmpty(drsOptionsConfiguration[requiredCapitaOption]))
                {
                    throw new InvalidOperationException($"Incorrect configuration: {nameof(CapitaOptions)}.{requiredCapitaOption} is a required configuration.");
                }
            }

            services.Configure<CapitaOptions>(drsOptionsConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HousingManagementSystemApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSentryTracing();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
