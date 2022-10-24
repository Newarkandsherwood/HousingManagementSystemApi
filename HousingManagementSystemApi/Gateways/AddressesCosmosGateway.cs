using System.Collections.Generic;
using System.Threading.Tasks;
using HACT.Dtos;
namespace HousingManagementSystemApi.Gateways
{
    using Helpers;

    public class AddressesCosmosGateway : IAddressesGateway
    {
        private readonly IContainerResolver cosmosContainerResolver;

        public AddressesCosmosGateway(IContainerResolver containerResolver) => this.cosmosContainerResolver = containerResolver;

        public async Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode,
            string repairType)
        {
            using var queryResultSetIterator = this.AddressQueryHelper(repairType).GetItemQueryIterator<PropertyAddress>(postcode);
            var addresses = new List<PropertyAddress>();

            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                addresses.AddRange(currentResultSet);
            }

            return addresses;
        }

        private ICosmosAddressQueryHelper AddressQueryHelper(string repairType) => new CosmosAddressQueryHelper(this.cosmosContainerResolver.Resolve(repairType));
    }
}
