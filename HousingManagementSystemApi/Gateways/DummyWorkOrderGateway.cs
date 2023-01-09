namespace HousingManagementSystemApi.Gateways;

using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;

public class DummyWorkOrderGateway : IWorkOrderGateway
{
    public Task<string> CreateWorkOrder(string locationId, string sorCode)
    {
        Guard.Against.NullOrWhiteSpace(locationId, nameof(locationId));
        Guard.Against.NullOrWhiteSpace(sorCode, nameof(sorCode));

        return Task.FromResult(Guid.NewGuid().ToString().GetHashCode().ToString("x").ToUpper());
    }
}
