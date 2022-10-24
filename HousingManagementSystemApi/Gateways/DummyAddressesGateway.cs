namespace HousingManagementSystemApi.Gateways
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HACT.Dtos;

    public class DummyAddressesGateway : IAddressesGateway
    {
        public Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode) =>
            Task.FromResult<IEnumerable<PropertyAddress>>(new[]
            {
                new PropertyAddress
                {
                    PostalCode = postcode,
                    BuildingNumber = "1",
                    Reference = new Reference { ID = "00000001", },
                    AddressLine = new[] { "Fake Road" },
                    CityName = "Fake City"
                },
                new PropertyAddress
                {
                    PostalCode = postcode,
                    Reference = new Reference { ID = "00000002", },
                    AddressLine = new[] { "2 Fake Road" },
                    CityName = "Fake City"
                }
            });

        public Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode, string repairType) => throw new System.NotImplementedException();
    }
}
