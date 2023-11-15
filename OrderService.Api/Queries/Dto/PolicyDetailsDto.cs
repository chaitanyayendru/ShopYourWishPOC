using System;
using System.Collections.Generic;

namespace OrderService.Api.Queries.Dto
{
    public class OrderDetailsDto
    {
        public string Number { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string OrderHolder { get; set; }
        public decimal TotalOrderValue { get; set; }
        public string ProductCode { get; set; }
        public string AccountNumber { get; set; }

        public List<string> Covers { get; set; }
    }
}