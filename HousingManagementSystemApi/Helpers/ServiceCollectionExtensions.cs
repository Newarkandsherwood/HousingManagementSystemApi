namespace HousingManagementSystemApi.Helpers;

using System.Collections.Generic;
using System.Linq;
// using Ardalis.GuardClauses;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddCosmosAddressContainers(this IServiceCollection services, IEnumerable<string> repairType)
    {
        // Guard.Against.NullOrWhiteSpace(repairType, nameof(repairType));
        // Guard.Against.InvalidInput(repairType, nameof(repairType), RepairType.IsValidValue);

        var cosmosAddressContainers = repairType.ToDictionary( x => x, GetCosmosClientContainer);

        services.AddTransient<IDictionary<string, Container>>(_ => cosmosAddressContainers);
        services.AddTransient<IContainerResolver, ContainerResolver>();
    }

    private static Container GetCosmosClientContainer(string repairType)
    {
        var cosmosClient = new CosmosClient(EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_ENDPOINT_URL"),
            EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_AUTHORIZATION_KEY"));

        return cosmosClient.GetContainer(EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_DATABASE_ID"),
            EnvironmentVariableHelper.GetEnvironmentVariable($"COSMOS_{repairType}_CONTAINER_ID"));
    }
}
