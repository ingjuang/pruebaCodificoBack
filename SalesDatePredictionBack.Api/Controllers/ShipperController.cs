using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionBack.Core.Interfaces.Services;

namespace SalesDatePredictionBack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _shipperService;

        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        [HttpGet]
        public IActionResult GetShippers()
        {
            return Ok(_shipperService.GetShippers());
        }
    }
}
