using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardService.Domain;
using Elasticsearch.Net;
using Nest;

namespace DashboardService.Init
{
    public class SalesData
    {
        private readonly ElasticClient elasticClient;

        public SalesData(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public async Task SeedData()
        {
            var orderIndexExists = await elasticClient.IndexExistsAsync("order_stats");
            if (orderIndexExists.Exists)
            {
                await elasticClient.DeleteIndexAsync(new DeleteIndexRequest("order_stats"));
            }

            var salesData = new Dictionary<string,(string Product, int Month, int Orders)[]> 
            {
                { "rohit.solid", RohitSalesData() },
                { "surya.solid", SuryaSalesData() },
                { "admin", AdminSalesData() }
            };

            foreach (var agentSalesData in salesData)
            {
                foreach (var (product, month, orders) in agentSalesData.Value)
                {
                    var startMonth = DateTime.Now.AddMonths(-1 * month);

                    for (var index = 0; index < orders; index++)
                    {
                        var order = new OrderInfo
                        (
                            Guid.NewGuid().ToString(),
                            new DateTime(startMonth.Year, startMonth.Month, 1),
                            new DateTime(startMonth.Year, startMonth.Month, 1).AddYears(1).AddDays(-1),
                            "Anonymous Mike",
                            product,
                            100M,
                            agentSalesData.Key
                        );

                        await elasticClient.IndexAsync
                        (
                            order,
                            i => i
                                .Index("order_stats")
                                .Id(order.Number)
                                .Refresh(Refresh.True)
                        );
                    }


                }
            }
        }

        private static (string Product, int Month, int Orders)[] RohitSalesData()
        {
            return new []
            {
                (Product: "Britania", Month: 1, Orders: 10),
                (Product: "Sunfeast", Month: 1, Orders: 9),
                (Product: "Tata", Month: 1, Orders: 8),
                (Product: "ThreeRoses", Month: 1, Orders: 7),
                
                (Product: "Britania", Month: 2, Orders: 6),
                (Product: "Sunfeast", Month: 2, Orders: 5),
                (Product: "Tata", Month: 2, Orders: 6),
                (Product: "ThreeRoses", Month: 2, Orders: 5),
                
                (Product: "Britania", Month: 3, Orders: 7),
                (Product: "Sunfeast", Month: 3, Orders: 5),
                (Product: "Tata", Month: 3, Orders: 8),
                (Product: "ThreeRoses", Month: 3, Orders: 8),
                
                (Product: "Britania", Month: 4, Orders: 10),
                (Product: "Sunfeast", Month: 4, Orders: 11),
                (Product: "Tata", Month: 4, Orders: 12),
                (Product: "ThreeRoses", Month: 4, Orders: 14),
                
                (Product: "Britania", Month: 5, Orders: 14),
                (Product: "Sunfeast", Month: 5, Orders: 14),
                (Product: "Tata", Month: 5, Orders: 10),
                (Product: "ThreeRoses", Month: 5, Orders: 10),
                
                (Product: "Britania", Month: 6, Orders: 14),
                (Product: "Sunfeast", Month: 6, Orders: 16),
                (Product: "Tata", Month: 6, Orders: 12),
                (Product: "ThreeRoses", Month: 6, Orders: 12),
                
                (Product: "Britania", Month: 7, Orders: 10),
                (Product: "Sunfeast", Month: 7, Orders: 10),
                (Product: "Tata", Month: 7, Orders: 8),
                (Product: "ThreeRoses", Month: 7, Orders: 9),
                
                (Product: "Britania", Month: 8, Orders: 10),
                (Product: "Sunfeast", Month: 8, Orders: 10),
                (Product: "Tata", Month: 8, Orders: 8),
                (Product: "ThreeRoses", Month: 8, Orders: 9),
                
                (Product: "Britania", Month: 9, Orders: 12),
                (Product: "Sunfeast", Month: 9, Orders: 12),
                (Product: "Tata", Month: 9, Orders: 12),
                (Product: "ThreeRoses", Month: 9, Orders: 11),
                
                (Product: "Britania", Month: 10, Orders: 14),
                (Product: "Sunfeast", Month: 10, Orders: 10),
                (Product: "Tata", Month: 10, Orders: 10),
                (Product: "ThreeRoses", Month: 10, Orders: 8),
                
                (Product: "Britania", Month: 11, Orders: 10),
                (Product: "Sunfeast", Month: 11, Orders: 8),
                (Product: "Tata", Month: 11, Orders: 8),
                (Product: "ThreeRoses", Month: 11, Orders: 6),
                
                (Product: "Britania", Month: 12, Orders: 9),
                (Product: "Sunfeast", Month: 12, Orders: 4),
                (Product: "Tata", Month: 12, Orders: 6),
                (Product: "ThreeRoses", Month: 12, Orders: 10),
            };
        }
        
        private static (string Product, int Month, int Orders)[] SuryaSalesData()
        {
            return new []
            {
                (Product: "Britania", Month: 1, Orders: 8),
                (Product: "Sunfeast", Month: 1, Orders: 7),
                (Product: "Tata", Month: 1, Orders: 6),
                (Product: "ThreeRoses", Month: 1, Orders: 5),
                
                (Product: "Britania", Month: 2, Orders: 4),
                (Product: "Sunfeast", Month: 2, Orders: 3),
                (Product: "Tata", Month: 2, Orders: 3),
                (Product: "ThreeRoses", Month: 2, Orders: 8),
                
                (Product: "Britania", Month: 3, Orders: 5),
                (Product: "Sunfeast", Month: 3, Orders: 5),
                (Product: "Tata", Month: 3, Orders: 3),
                (Product: "ThreeRoses", Month: 3, Orders: 12),
                
                (Product: "Britania", Month: 4, Orders: 3),
                (Product: "Sunfeast", Month: 4, Orders: 3),
                (Product: "Tata", Month: 4, Orders: 3),
                (Product: "ThreeRoses", Month: 4, Orders: 3),
                
                (Product: "Britania", Month: 5, Orders: 10),
                (Product: "Sunfeast", Month: 5, Orders: 7),
                (Product: "Tata", Month: 5, Orders: 7),
                (Product: "ThreeRoses", Month: 5, Orders: 7),
                
                (Product: "Britania", Month: 6, Orders: 12),
                (Product: "Sunfeast", Month: 6, Orders: 10),
                (Product: "Tata", Month: 6, Orders: 12),
                (Product: "ThreeRoses", Month: 6, Orders: 12),
                
                (Product: "Britania", Month: 7, Orders: 5),
                (Product: "Sunfeast", Month: 7, Orders: 10),
                (Product: "Tata", Month: 7, Orders: 5),
                (Product: "ThreeRoses", Month: 7, Orders: 4),
                
                (Product: "Britania", Month: 8, Orders: 6),
                (Product: "Sunfeast", Month: 8, Orders: 11),
                (Product: "Tata", Month: 8, Orders: 6),
                (Product: "ThreeRoses", Month: 8, Orders: 8),
                
                (Product: "Britania", Month: 9, Orders: 8),
                (Product: "Sunfeast", Month: 9, Orders: 15),
                (Product: "Tata", Month: 9, Orders: 2),
                (Product: "ThreeRoses", Month: 9, Orders: 2),
                
                (Product: "Britania", Month: 10, Orders: 8),
                (Product: "Sunfeast", Month: 10, Orders: 10),
                (Product: "Tata", Month: 10, Orders: 2),
                (Product: "ThreeRoses", Month: 10, Orders: 4),
                
                (Product: "Britania", Month: 11, Orders: 10),
                (Product: "Sunfeast", Month: 11, Orders: 12),
                (Product: "Tata", Month: 11, Orders: 4),
                (Product: "ThreeRoses", Month: 11, Orders: 4),
                
                (Product: "Britania", Month: 12, Orders: 8),
                (Product: "Sunfeast", Month: 12, Orders: 2),
                (Product: "Tata", Month: 12, Orders: 2),
                (Product: "ThreeRoses", Month: 12, Orders: 5),
            };
        }
        
        private static (string Product, int Month, int Orders)[] AdminSalesData()
        {
            return new []
            {
                (Product: "Britania", Month: 1, Orders: 7),
                (Product: "Sunfeast", Month: 1, Orders: 6),
                (Product: "Tata", Month: 1, Orders: 5),
                (Product: "ThreeRoses", Month: 1, Orders: 4),
                
                (Product: "Britania", Month: 2, Orders: 3),
                (Product: "Sunfeast", Month: 2, Orders: 2),
                (Product: "Tata", Month: 2, Orders: 2),
                (Product: "ThreeRoses", Month: 2, Orders: 7),
                
                (Product: "Britania", Month: 3, Orders: 4),
                (Product: "Sunfeast", Month: 3, Orders: 4),
                (Product: "Tata", Month: 3, Orders: 2),
                (Product: "ThreeRoses", Month: 3, Orders: 5),
                
                (Product: "Britania", Month: 4, Orders: 2),
                (Product: "Sunfeast", Month: 4, Orders: 2),
                (Product: "Tata", Month: 4, Orders: 2),
                (Product: "ThreeRoses", Month: 4, Orders: 2),
                
                (Product: "Britania", Month: 5, Orders: 1),
                (Product: "Sunfeast", Month: 5, Orders: 5),
                (Product: "Tata", Month: 5, Orders: 5),
                (Product: "ThreeRoses", Month: 5, Orders: 5),
                
                (Product: "Britania", Month: 6, Orders: 5),
                (Product: "Sunfeast", Month: 6, Orders: 3),
                (Product: "Tata", Month: 6, Orders: 3),
                (Product: "ThreeRoses", Month: 6, Orders: 3),
                
                (Product: "Britania", Month: 7, Orders: 4),
                (Product: "Sunfeast", Month: 7, Orders: 4),
                (Product: "Tata", Month: 7, Orders: 4),
                (Product: "ThreeRoses", Month: 7, Orders: 4),
                
                (Product: "Britania", Month: 8, Orders: 6),
                (Product: "Sunfeast", Month: 8, Orders: 6),
                (Product: "Tata", Month: 8, Orders: 6),
                (Product: "ThreeRoses", Month: 8, Orders: 6),
                
                (Product: "Britania", Month: 9, Orders: 2),
                (Product: "Sunfeast", Month: 9, Orders: 4),
                (Product: "Tata", Month: 9, Orders: 2),
                (Product: "ThreeRoses", Month: 9, Orders: 2),
                
                (Product: "Britania", Month: 10, Orders: 4),
                (Product: "Sunfeast", Month: 10, Orders: 5),
                (Product: "Tata", Month: 10, Orders: 1),
                (Product: "ThreeRoses", Month: 10, Orders: 1),
                
                (Product: "Britania", Month: 11, Orders: 1),
                (Product: "Sunfeast", Month: 11, Orders: 1),
                (Product: "Tata", Month: 11, Orders: 4),
                (Product: "ThreeRoses", Month: 11, Orders: 4),
                
                (Product: "Britania", Month: 12, Orders: 5),
                (Product: "Sunfeast", Month: 12, Orders: 2),
                (Product: "Tata", Month: 12, Orders: 2),
                (Product: "ThreeRoses", Month: 12, Orders: 2),
            };
        }
    }
}