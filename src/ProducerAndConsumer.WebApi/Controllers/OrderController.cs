using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProducerAndConsumer.WebApi.Models;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

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
                #region Inserir na fila

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "orderQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = JsonSerializer.Serialize(order);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "orderQueue",
                                         basicProperties: null,
                                         body: body);
                }

                #endregion

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
