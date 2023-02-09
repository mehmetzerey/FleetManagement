using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using FleetManagement.Application.Const;
using FleetManagement.Application.Features.Commands.CreateShipment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FleetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IMemoryCache _memoryCache;
        public ShipmentController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        // POST api/<ShipmentController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateShipmentCommandRequest request)
        {
            if (_memoryCache.TryGetValue(Constant.MemKey, out object list))
                return Ok((CreateShipmentCommandResponse)list);

            var result = await _mediator.Send(request);

            _memoryCache.Set(Constant.MemKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(20),
                Priority = CacheItemPriority.Normal
            });

            return Ok(result);
        }
    }
}
