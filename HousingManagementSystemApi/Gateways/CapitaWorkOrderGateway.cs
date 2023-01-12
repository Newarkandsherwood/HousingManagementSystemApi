using System.Threading.Tasks;
using Ardalis.GuardClauses;

namespace HousingManagementSystemApi.Gateways;

using Services;

public class CapitaWorkOrderGateway : IWorkOrderGateway
{
    private readonly ICapitaService capitaService;

    public CapitaWorkOrderGateway(ICapitaService capitaService)
    {
        this.capitaService = capitaService;
    }
    public async Task<string> CreateWorkOrder(string description, string locationId, string sorCode)
    {
        Guard.Against.NullOrWhiteSpace(description, nameof(locationId));
        Guard.Against.NullOrWhiteSpace(locationId, nameof(locationId));
        Guard.Against.NullOrWhiteSpace(sorCode, nameof(sorCode));

        var result = await capitaService.LogJob(description, locationId, sorCode);

        return result;
    }
}
