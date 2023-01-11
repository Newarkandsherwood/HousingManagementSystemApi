namespace HousingManagementSystemApi.UseCases;

using System.Threading.Tasks;

public interface ICreateWorkOrderUseCase
{
    Task<string> Execute(string description, string locationId, string sorCode);
}
