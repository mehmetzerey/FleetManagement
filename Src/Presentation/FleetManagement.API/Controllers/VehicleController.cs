
using Microsoft.AspNetCore.Mvc;
using FleetManagement.Application.Features.Commands.CreateVehicle;
using FleetManagement.Application.Features.Queries.GetAllVehicle;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FleetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<VehicleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllVehicleQueryRequest());
            return Ok(result);
        }


        // POST api/<VehicleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateVehicleCommandRequest createVehicle)
        {
            var result = await _mediator.Send(createVehicle);
            return Ok(result);
        }
    }
}
