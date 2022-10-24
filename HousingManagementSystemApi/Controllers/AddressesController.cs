using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace HousingManagementSystemApi.Controllers
{
    using System;
    using Sentry;
    using UseCases;

    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IRetrieveAddressesUseCase retrieveAddressesUseCase;

        public AddressesController(IRetrieveAddressesUseCase retrieveAddressesUseCase)
        {
            this.retrieveAddressesUseCase = retrieveAddressesUseCase;
        }

        [HttpGet]
        [Route("TenantAddresses")]
        public async Task<IActionResult> TenantAddresses([FromQuery] string postcode)
        {
            try
            {
                var result = await retrieveAddressesUseCase.Execute(postcode, "TENANT");
                return Ok(result);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("CommunalAddresses")]
        public async Task<IActionResult> CommunalAddresses([FromQuery] string postcode)
        {
            try
            {
                var result = await retrieveAddressesUseCase.Execute(postcode, "COMMUNAL");
                return Ok(result);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
