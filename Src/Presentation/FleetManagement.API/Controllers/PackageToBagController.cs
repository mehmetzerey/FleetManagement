using Microsoft.AspNetCore.Mvc;
using FleetManagement.Application.Features.Commands.CreatePackageToBag;
using FleetManagement.Application.Features.Queries.GetAllPackagesToBags;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FleetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageToBagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackageToBagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PackageToBagController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllPackagesToBagsQueryRequest());
            return Ok(result);
        }


        // POST api/<PackageToBagController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePackagesToBagsCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
