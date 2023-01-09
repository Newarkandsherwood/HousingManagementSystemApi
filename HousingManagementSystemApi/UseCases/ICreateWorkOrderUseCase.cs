namespace HousingManagementSystemApi.UseCases;

using System.Threading.Tasks;

public interface ICreateWorkOrderUseCase
{
    Task<string> Execute(string locationId, string sorCode);
}
