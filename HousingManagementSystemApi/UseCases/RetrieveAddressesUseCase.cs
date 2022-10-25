using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HACT.Dtos;
using HousingManagementSystemApi.Gateways;

namespace HousingManagementSystemApi.UseCases
{
    public class RetrieveAddressesUseCase : IRetrieveAddressesUseCase
    {
        private readonly IAddressesGateway addressesGateway;

        public RetrieveAddressesUseCase(IAddressesGateway addressesGateway)
        {
            this.addressesGateway = addressesGateway;
        }

        public async Task<IEnumerable<PropertyAddress>> Execute(string postcode, string repairType)
        {
            if (postcode == null)
                throw new ArgumentNullException(nameof(postcode));

            if (postcode == "")
                return new List<PropertyAddress>();

            if (repairType == null)
                throw new ArgumentNullException(nameof(repairType));

            if (repairType == "")
                throw new ArgumentException("Repair type must be a valid value");

            var result = await addressesGateway.SearchByPostcode(postcode, repairType);
            return result;
        }
    }
}
