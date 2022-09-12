namespace HousingManagementSystemApi.Helpers;

using HACT.Dtos;
using Microsoft.Azure.Cosmos;

public interface ICosmosAddressQueryHelper
{
    FeedIterator<PropertyAddress> GetItemQueryIterator<T>(string postcode);
}
