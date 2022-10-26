using System.Collections.Generic;
using System.Threading.Tasks;
using HACT.Dtos;
using HousingManagementSystemApi.Gateways;

namespace HousingManagementSystemApi.UseCases
{
    using Ardalis.GuardClauses;
    using Helpers;

    public class RetrieveAddressesUseCase : IRetrieveAddressesUseCase
    {
        private readonly IAddressesGateway addressesGateway;

        public RetrieveAddressesUseCase(IAddressesGateway addressesGateway)
        {
            this.addressesGateway = addressesGateway;
        }

        public async Task<IEnumerable<PropertyAddress>> Execute(string postcode, string repairType)
        {
            Guard.Against.Null(postcode, nameof(postcode));
            Guard.Against.Null(repairType, nameof(repairType));
            Guard.Against.InvalidInput(repairType, nameof(repairType), RepairType.IsValidValue);

            if (postcode == "")
            {
                return new List<PropertyAddress>();
            }

            var result = await addressesGateway.SearchByPostcode(postcode, repairType);
            return result;
        }
    }
}
