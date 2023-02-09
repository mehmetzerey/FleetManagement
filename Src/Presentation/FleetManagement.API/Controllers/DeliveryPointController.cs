using Microsoft.AspNetCore.Mvc;
using FleetManagement.Application.Features.Commands.CreateDeliveryPoint;
using FleetManagement.Application.Features.Queries.GetAllDeliveryPoint;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FleetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPointController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryPointController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<DeliveryPointController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllDeliveryPointQueryRequest());
            return Ok(result);
        }

        // POST api/<DeliveryPointController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDeliveryPointCommandRequest commandRequest)
        {
            var result = await _mediator.Send(commandRequest);
            return Ok(result);
        }

    }
}
