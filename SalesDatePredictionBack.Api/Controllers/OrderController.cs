using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionBack.Core.Entities;
using SalesDatePredictionBack.Core.Interfaces.Services;

namespace SalesDatePredictionBack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrders(int custId)
        {
            return Ok(_orderService.GetOrders(custId));
        }

        [HttpGet("sale-date-prediction")]
        public IActionResult GetSaleDatePrediction(string companyName = null)
        {
            return Ok(_orderService.GetSaleDatePrediction(companyName));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderWithProducts([FromBody] OrderDTO orderDTO)
        {
            await _orderService.AddOrderWithProductsAsync(orderDTO);
            return Ok();
        }
    }
}
