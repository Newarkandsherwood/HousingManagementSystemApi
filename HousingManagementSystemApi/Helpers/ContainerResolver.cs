namespace HousingManagementSystemApi.Helpers;

using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Microsoft.Azure.Cosmos;

public class ContainerResolver : IContainerResolver
{
    private readonly IDictionary<string, Container> cosmosAddressContainers;

    public ContainerResolver(IDictionary<string, Container> cosmosAddressContainers)
    {
        // Guard.Against.Null(cosmosAddressContainers, nameof(cosmosAddressContainers));

        this.cosmosAddressContainers = cosmosAddressContainers;
    }

    public Container Resolve(string repairType)
    {
        // Guard.Against.NullOrWhiteSpace(repairType, nameof(repairType));
        // Guard.Against.InvalidInput(repairType, nameof(repairType), RepairType.IsValidValue);

        if (!cosmosAddressContainers.TryGetValue(repairType, out var result))
        {
            throw new NotSupportedException($"Cosmos DB container for repair Type '{repairType}' not configured");
        }

        return result;
    }
}
