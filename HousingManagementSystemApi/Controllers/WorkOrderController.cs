using System;
using System.Threading.Tasks;
using HousingManagementSystemApi.UseCases;
using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace HousingManagementSystemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkOrderController : ControllerBase
    {
        private readonly ICreateWorkOrderUseCase createWorkOrderUseCase;
        public WorkOrderController(ICreateWorkOrderUseCase createWorkOrderUseCase)
        {
            this.createWorkOrderUseCase = createWorkOrderUseCase;
        }

        [HttpPost]
        [Route(nameof(CreateWorkOrder))]
        public async Task<IActionResult> CreateWorkOrder([FromBody] string description, [FromQuery] string locationId, [FromQuery] string sorCode)
        {
            try
            {
                var result = await createWorkOrderUseCase.Execute(description, locationId, sorCode);
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
