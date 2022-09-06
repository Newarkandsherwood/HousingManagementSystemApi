using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HACT.Dtos;
namespace HousingManagementSystemApi.Gateways
{
    using System;
    using Helpers;
    using Microsoft.Azure.Cosmos;

    public class AddressesCosmosGateway : IAddressesGateway, IDisposable
    {
        private CosmosClient cosmosClient;
        private string cosmosDatabaseId;
        private string containerId;
        // Use read only keys here - we do not need to write to the DB
        private string  endpointUri;
        private string primaryKey;

        public AddressesCosmosGateway()
        {
            this.cosmosDatabaseId = EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_DATABASE_ID");
            this.containerId = EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_CONTAINER_ID");
            this.endpointUri = EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_ENDPOINT_URL");
            this.primaryKey = EnvironmentVariableHelper.GetEnvironmentVariable("COSMOS_AUTHORIZATION_KEY");
            this.cosmosClient = new CosmosClient(endpointUri, primaryKey);
        }

        public async Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode)
        {

            var container = this.cosmosClient.GetContainer(cosmosDatabaseId, this.containerId);
            var queryDefinition = new QueryDefinition("SELECT * FROM c WHERE (UPPER(REPLACE(c.PostalCode, ' ','')))  = (UPPER(REPLACE(@postcode, ' ','')))  ORDER BY c.AddressLine[0] ASC")
                                            .WithParameter("@postcode", postcode);

            using FeedIterator<PropertyAddress> queryResultSetIterator = container.GetItemQueryIterator<PropertyAddress>(queryDefinition);
            List<PropertyAddress> addresses = new List<PropertyAddress>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<PropertyAddress> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (PropertyAddress address in currentResultSet)
                {
                    addresses.Add(address);
                }
            }

            return addresses;
        }

        public void Dispose() => this.cosmosClient?.Dispose();
    }
}
