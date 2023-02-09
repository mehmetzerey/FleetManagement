
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using FleetManagement.Application.Features.Commands.CreateBag;
using FleetManagement.Application.Features.Queries.GetAllBag;

namespace FleetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BagController>
        [HttpGet(Name = "GetList")]
        public async Task<IActionResult> GetList()
        {
            var getAllBagQueryResponse = await _mediator.Send(new GetAllBagQueryRequest());
            return Ok(getAllBagQueryResponse);
        }
        // POST api/<BagController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBagCommandRequest bagCommandRequest)
        {
            CreateBagCommandResponse response = await _mediator.Send(bagCommandRequest);
            return Ok(response); 
        }
    }
}
