using System;
using System.Linq;
using DashboardService.Domain;
using Elasticsearch.Net;
using Nest;

namespace DashboardService.DataAccess.Elastic
{
    public class ElasticOrderRepository : IOrderRepository
    {
        private readonly ElasticClient elasticClient;

        public ElasticOrderRepository(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public void Save(OrderInfo order)
        {
            var response = elasticClient.Index
            (
                order, 
                i => i
                    .Index("order_stats")
                    .Id(order.Number)
                    .Refresh(Refresh.True)
            );

            if (!response.IsValid)
            {
                throw new ApplicationException("Failed to index a order document");
            }
        }

        public OrderInfo FindByNumber(string orderNumber)
        {
            var searchResponse = elasticClient.Search<OrderInfo>
            (
                s => s
                    .Query(q => q
                        .Bool(b => b
                            .Filter(bf => bf
                                .Term(
                                    new Field("number.keyword"),orderNumber)))
                    )
            );

            return searchResponse.Documents.FirstOrDefault();
        }

        public AgentSalesQueryResult GetAgentSales(AgentSalesQuery query)
        {
            var adapter = AgentSalesQueryAdapter.For(query);
            var response = elasticClient.Search<OrderInfo>(adapter.BuildQuery());
            return adapter.ExtractResult(response);
        }

        public TotalSalesQueryResult GetTotalSales(TotalSalesQuery query)
        {
            var adapter = TotalSalesQueryAdapter.For(query);
            var response = elasticClient.Search<OrderInfo>(adapter.BuildQuery());
            return adapter.ExtractResult(response);
        }

        public SalesTrendsResult GetSalesTrend(SalesTrendsQuery query)
        {
            var adapter = SalesTrendsQueryAdapter.For(query);
            var response = elasticClient.Search<OrderInfo>(adapter.BuildQuery());
            return adapter.ExtractResult(response);
        }
    }
}