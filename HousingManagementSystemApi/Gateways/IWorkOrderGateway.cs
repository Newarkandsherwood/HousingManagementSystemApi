namespace HousingManagementSystemApi.Gateways;

using System.Threading.Tasks;

public interface IWorkOrderGateway
{
    Task<string> CreateWorkOrder(string locationId, string sorCode);
}
