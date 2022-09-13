using System.Collections.Generic;
using System.Threading.Tasks;
using HACT.Dtos;
namespace HousingManagementSystemApi.Gateways
{
    using Helpers;

    public class AddressesCosmosGateway : IAddressesGateway
    {
        private ICosmosAddressQueryHelper addressQueryHelper;

        public AddressesCosmosGateway(ICosmosAddressQueryHelper cosmosAddressQueryHelper)
        {
            addressQueryHelper = cosmosAddressQueryHelper;
        }

        public async Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode)
        {
            using var queryResultSetIterator = addressQueryHelper.GetItemQueryIterator<PropertyAddress>(postcode);
            var addresses = new List<PropertyAddress>();

            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                addresses.AddRange(currentResultSet);
            }

            return addresses;
        }
    }
}
