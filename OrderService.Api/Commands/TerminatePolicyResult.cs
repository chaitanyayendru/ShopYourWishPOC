namespace OrderService.Api.Commands
{
    public class TerminateOrderResult
    {
        public string OrderNumber { get; set; }
        public decimal MoneyToReturn { get; set; }
    }
}