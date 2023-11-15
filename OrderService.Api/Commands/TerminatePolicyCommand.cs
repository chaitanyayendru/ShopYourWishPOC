using System;
using MediatR;

namespace OrderService.Api.Commands
{
    public class TerminateOrderCommand : IRequest<TerminateOrderResult>
    {
        public string OrderNumber { get; set; }
        public DateTime TerminationDate { get; set; }
    }
}