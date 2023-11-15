using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Commands;
using OrderService.Api.Queries;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator bus;

        public OrderController(IMediator bus)
        {
            this.bus = bus;
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrderCommand cmd)
        {
            var result = await bus.Send(cmd);
            return new JsonResult(result);
        }
        
        // GET 
        [HttpGet("{orderNumber}")]
        public async Task<ActionResult> Get(string orderNumber)
        {
            var result = await bus.Send(new GetOrderDetailsQuery { OrderNumber = orderNumber});
            return new JsonResult(result);
        }
        
        // DELETE
        [HttpDelete("/terminate")]
        public async Task<ActionResult> Post([FromBody] TerminateOrderCommand cmd)
        {
            var result = await bus.Send(cmd);
            return new JsonResult(result);
        }
    }
}