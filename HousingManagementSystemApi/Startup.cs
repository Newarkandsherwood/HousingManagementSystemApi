using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HousingManagementSystemApi
{
    using Gateways;
    using Helpers;
    using HousingRepairsOnline.Authentication.DependencyInjection;
    using Microsoft.Azure.Cosmos;
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

            services.AddHttpClient();
            // services.AddTransient<IAddressesGateway, AddressesDatabaseGateway>();
            // services.AddTransient<IAddressesGateway, DummyAddressesGateway>();
            services.AddTransient<IAddressesGateway, AddressesCosmosGateway>();

            services.AddTransient<ICosmosAddressQueryHelper, CosmosAddressQueryHelper>(s => new CosmosAddressQueryHelper(getCosmosClientContainer()));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HousingManagementSystemApi", Version = "v1" });
                c.AddJwtSecurityScheme();
            });

            services.AddHealthChecks();
            // .AddSqlServer(connectionString, name: "Universal Housing database");
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

        private static Container getCosmosClientContainer()
        {
            var cosmosClient = new CosmosClient(EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_ENDPOINT_URL"),
                EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_AUTHORIZATION_KEY"));

            return cosmosClient.GetContainer(EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_DATABASE_ID"),
                EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_TENANT_CONTAINER_ID"));
        }

        private static string GetEnvironmentVariable(string name) =>
            Environment.GetEnvironmentVariable(name) ??
            throw new InvalidOperationException($"Incorrect configuration: '{name}' environment variable must be set");
    }
}
