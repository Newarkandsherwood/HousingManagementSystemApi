using System.Collections.Generic;
using System.Threading.Tasks;
using HACT.Dtos;
namespace HousingManagementSystemApi.Gateways
{
    public interface IAddressesGateway
    {
        public Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode) => this.SearchByPostcode(postcode, null);

        public Task<IEnumerable<PropertyAddress>> SearchByPostcode(string postcode, string repairType);
    }
}
