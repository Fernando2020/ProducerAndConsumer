using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProducerAndConsumer.WebApi.Models;
using System;

namespace ProducerAndConsumer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult InsertOrder([FromBody]Order order)
        {
            try
            {
                return Accepted(order);
            }
            catch(Exception e)
            {
                _logger.LogError($"Ocorreu ao tentar criar uma nova ordem.", e);
                
                return StatusCode(500, e.Message);
            }
        }
    }
}
