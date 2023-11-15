namespace DashboardService.Domain
{
    public class SalesResult
    {
        public long OrdersCount { get; }
        public decimal OrderValue { get; }

        public SalesResult(long ordersCount, decimal orderValue)
        {
            OrdersCount = ordersCount;
            OrderValue = orderValue;
        }

        public override string ToString()
        {
            return $"count: {OrdersCount} amount: {OrderValue}";
        }

        public static SalesResult NoSale()
        {
            return new SalesResult(0,0M);
        }
    }
}