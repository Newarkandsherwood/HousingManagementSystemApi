using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HACT.Dtos;
namespace HousingManagementSystemApi.Gateways
{
    using Microsoft.Azure.Cosmos;

    public class AddressesCosmosGateway : IAddressesGateway
    {
        private CosmosClient cosmosClient;
        private string cosmosDatabaseId = "hrocosmosdb";
        private string containerId;
        private string endpointUri;
        private string primaryKey;

        public AddressesCosmosGateway()
        {
            cosmosClient = new CosmosClient(endpointUri, primaryKey);

        }

        public async Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode)
        {
            // Need to return a list of PropertyAddress. This gets converted to a UI address in the calling API, housing-repairs-online-API
            var database = this.cosmosClient.GetDatabase(this.cosmosDatabaseId);

            var sqlQueryText = @"
            {
                ""query"": ""SELECT * FROM Addresses a WHERE (UPPER(REPLACE(c.PostalCode, ' ','')))  = (UPPER(REPLACE(@postcode, ' ','')))  ORDER BY c.AddressLine[0] ASC"",
                ""parameters"": [
                {""name"": ""@postcode"", ""value"": ""{postcode}""}
                ]
            }";
            var container = database.GetContainer(this.containerId);
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
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
            // Sample JSON

            // "Reference": {
            //     "ID": "1000546",
            //     "AllocatedBy": "Capita"
            // },
            // "AddressLine": [
            // "Block: Abbey 1 (flats 18-24), Abbey Road, Edwinstowe"
            //     ],
            // "PostalCode": "NG21 9LQ",
            // "id": "81086721-b6b9-40dc-9c76-80b9aa3aa770",
            // "_rid": "zkksAK6uLYcBAAAAAAAAAA==",
            // "_self": "dbs/zkksAA==/colls/zkksAK6uLYc=/docs/zkksAK6uLYcBAAAAAAAAAA==/",
            // "_etag": "\"0d006711-0000-1000-0000-630f19500000\"",
            // "_attachments": "attachments/",
            // "_ts": 1661933904




            // return Task.FromResult<IEnumerable<PropertyAddress>>(new[]
            //     {
            //         new PropertyAddress
            //         {
            //             PostalCode = postcode,
            //             BuildingNumber = "1",
            //             Reference = new Reference { ID = "00000001", },
            //             AddressLine = new[] { "Fake Road" },
            //             CityName = "Fake City"
            //         },
            //         new PropertyAddress
            //         {
            //             PostalCode = postcode,
            //             Reference = new Reference { ID = "00000002", },
            //             AddressLine = new[] { "2 Fake Road" },
            //             CityName = "Fake City"
            //         }
            //     });
        }
    }
}
