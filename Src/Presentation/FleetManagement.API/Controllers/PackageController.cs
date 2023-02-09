using Microsoft.AspNetCore.Mvc;
using FleetManagement.Application.Features.Commands.CreatePackage;
using FleetManagement.Application.Features.Queries.GetAllPackage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FleetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PackageController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllPackageQueryRequest());
            return Ok(response);
        }


        // POST api/<PackageController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePackageCommandRequest createPackage)
        {
            var result = await _mediator.Send(createPackage);
            return Ok(result);
        }

    }
}
