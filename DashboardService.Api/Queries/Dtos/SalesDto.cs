namespace DashboardService.Api.Queries.Dtos
{
    public class SalesDto
    {
        public long OrdersCount { get; set; }
        public decimal OrderAmount { get; set; }

        public SalesDto()
        {
        }

        public SalesDto(long ordersCount, decimal premiumAmount)
        {
            OrdersCount = ordersCount;
            OrderAmount = premiumAmount;
        }
    }
}