using MediatR;
using OrderService.Api.Commands.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Api.Events
{
    public class OrderCreated : INotification
    {
        public string OrderNumber { get; set; }
        public string ProductCode { get; set; }
        public DateTime OrderFrom { get; set; }
        public DateTime OrderTo { get; set; }
        public PersonDto OrderHolder { get; set; }
        public decimal TotalOrderValue { get; set; }
        public string AgentLogin { get; set; }
    }
}
