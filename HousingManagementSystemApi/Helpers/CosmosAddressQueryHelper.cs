namespace HousingManagementSystemApi.Helpers;

using HACT.Dtos;
using Microsoft.Azure.Cosmos;

public class CosmosAddressQueryHelper : ICosmosAddressQueryHelper
{
    private readonly Container cosmosContainer;
    public CosmosAddressQueryHelper(Container container) => this.cosmosContainer = container;

    public FeedIterator<PropertyAddress> GetItemQueryIterator<T>(string postcode) =>
        this.cosmosContainer.GetItemQueryIterator<PropertyAddress>(GetQueryDefinition(postcode));

    private static QueryDefinition GetQueryDefinition(string postcode)
    {
        var query =
            "SELECT * FROM c WHERE (UPPER(REPLACE(c.PostalCode, ' ','')))  = (UPPER(REPLACE(@postcode, ' ','')))  ORDER BY c.AddressLine[0] ASC";
        return new QueryDefinition(query)
            .WithParameter("@postcode", postcode);
    }
}
