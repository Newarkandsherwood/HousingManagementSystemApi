namespace HousingManagementSystemApi.Services;

using System.Threading.Tasks;

public interface ICapitaService
{
    public Task<string> LogJob(string description, string locationId, string sorCode);
}
