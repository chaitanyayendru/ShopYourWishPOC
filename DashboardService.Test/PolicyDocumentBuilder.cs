using System;
using DashboardService.Domain;

namespace DashboardService.Test
{
    public class OrderDocumentBuilder
    {
        private string number;
        private DateTime from;
        private DateTime to;
        private string orderHolder;
        private string productCode;
        private decimal totalPremium;
        private string agentLogin;
        
        public static OrderDocumentBuilder Create() => new OrderDocumentBuilder();

        public OrderDocumentBuilder()
        {
            number = Guid.NewGuid().ToString();
            from = new DateTime(2020,1,1);
            to = from.AddYears(1).AddDays(-1);
            orderHolder = "Jan Test";
            productCode = "Britania";
            totalPremium = 100M;
            agentLogin = "rohit.solid";
        }

        public OrderDocumentBuilder WithNumber(string orderNumber)
        {
            number = orderNumber;
            return this;
        }
        
        public OrderDocumentBuilder WithDates(string start, string end)
        {
            from = DateTime.Parse(start);
            to = DateTime.Parse(end);
            return this;
        }

        public OrderDocumentBuilder WithProduct(string product)
        {
            productCode = product;
            return this;
        }

        public OrderDocumentBuilder WithPremium(decimal premium)
        {
            totalPremium = premium;
            return this;
        }
        
        public OrderDocumentBuilder WithAgent(string agent)
        {
            agentLogin = agent;
            return this;
        }
        
        public OrderInfo Build()
        {
            return new OrderInfo
            (
                number,
                @from,
                to,
                orderHolder,
                productCode,
                totalPremium,
                agentLogin
            );
        }
    }
}