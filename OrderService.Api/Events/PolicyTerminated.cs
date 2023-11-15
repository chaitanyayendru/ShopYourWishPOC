using System;
using MediatR;
using OrderService.Api.Commands.Dtos;

namespace OrderService.Api.Events
{
    public class OrderTerminated : INotification
    {
        public string OrderNumber { get; set; }
        public string ProductCode { get; set; }
        public DateTime OrderFrom { get; set; }
        public DateTime OrderTo { get; set; }
        public PersonDto OrderHolder { get; set; }
        public decimal TotalOrderValue { get; set; }
        public decimal AmountToReturn { get; set; }
    }
}