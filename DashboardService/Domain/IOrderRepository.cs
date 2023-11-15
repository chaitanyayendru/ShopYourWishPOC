namespace DashboardService.Domain
{
    public interface IOrderRepository
    {
        void Save(OrderInfo order);

        OrderInfo FindByNumber(string orderNumber);

        AgentSalesQueryResult GetAgentSales(AgentSalesQuery query);

        TotalSalesQueryResult GetTotalSales(TotalSalesQuery query);

        SalesTrendsResult GetSalesTrend(SalesTrendsQuery query);
    }
}