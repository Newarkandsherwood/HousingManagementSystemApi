using System.Collections.Generic;
using System.Threading.Tasks;
using HACT.Dtos;
namespace HousingManagementSystemApi.Gateways
{
    using Helpers;
    using Microsoft.Azure.Cosmos;

    public class AddressesCosmosGateway : IAddressesGateway
    {
        private ICosmosAddressQueryHelper cosmosAddressQueryHelper;

        public AddressesCosmosGateway(ICosmosAddressQueryHelper cosmosAddressQueryHelper)
        {
            this.cosmosAddressQueryHelper = cosmosAddressQueryHelper;
        }

        public async Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode)
        {
            using var queryResultSetIterator = this.cosmosAddressQueryHelper.GetItemQueryIterator<PropertyAddress>(postcode);
            var addresses = new List<PropertyAddress>();

            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (var address in currentResultSet)
                {
                    addresses.Add(address);
                }
            }

            return addresses;
        }
    }
}
