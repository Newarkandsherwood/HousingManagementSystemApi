namespace HousingManagementSystemApi.Gateways
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using HACT.Dtos;
    using Repositories;

    public class AddressesDatabaseGateway : IAddressesGateway
    {
        private readonly IAddressesRepository addressesRepository;

        public AddressesDatabaseGateway(IAddressesRepository addressesRepository)
        {
            this.addressesRepository = addressesRepository;
        }

        public Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode)
        {
            Guard.Against.NullOrWhiteSpace(postcode, nameof(postcode));

            return addressesRepository.GetAddressesByPostcode(postcode);
        }

        public Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode, string repairType) => throw new System.NotImplementedException();
    }
}
