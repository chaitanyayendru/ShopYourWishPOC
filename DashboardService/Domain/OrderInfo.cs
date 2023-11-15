using System;

namespace DashboardService.Domain
{
    public class OrderInfo
    {
        public string Number { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public string OrderDistributor { get; private set; }
        public string ProductCode { get; private set; }
        public decimal TotalOrderValue { get; private set; }
        public string AgentLogin { get; private set; }
        
        public OrderInfo(string number, DateTime @from, DateTime to, string orderDistributor, string productCode, decimal totalOrderValue, string agentLogin)
        {
            Number = number;
            From = @from;
            To = to;
            OrderDistributor = orderDistributor;
            ProductCode = productCode;
            TotalOrderValue = totalOrderValue;
            AgentLogin = agentLogin;
        }
    }
}