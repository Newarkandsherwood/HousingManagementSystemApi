namespace HousingManagementSystemApi.Helpers;
using Microsoft.Azure.Cosmos;

public interface IContainerResolver
{
    public Container Resolve(string repairType);
}
