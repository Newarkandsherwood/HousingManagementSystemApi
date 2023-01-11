namespace HousingManagementSystemApi.Gateways;

using System.Threading.Tasks;

public interface IWorkOrderGateway
{
    Task<string> CreateWorkOrder(string description, string locationId, string sorCode);
}
