namespace HousingManagementSystemApi.UseCases;

using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Gateways;

public class CreateWorkOrderUseCase : ICreateWorkOrderUseCase
{
    private readonly IWorkOrderGateway workOrderGateway;

    public CreateWorkOrderUseCase(IWorkOrderGateway workOrderGateway)
    {
        this.workOrderGateway = workOrderGateway;
    }

    public Task<string> Execute(string description, string locationId, string sorCode)
    {
        Guard.Against.NullOrWhiteSpace(description, nameof(description));
        Guard.Against.NullOrWhiteSpace(locationId, nameof(locationId));
        Guard.Against.NullOrWhiteSpace(sorCode, nameof(sorCode));

        return workOrderGateway.CreateWorkOrder(description, locationId, sorCode);
    }

}
