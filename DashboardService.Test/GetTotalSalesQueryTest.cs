using System;
using System.Threading;
using System.Threading.Tasks;
using DashboardService.Api.Queries;
using DashboardService.DataAccess.Elastic;
using DashboardService.Queries;
using Xunit;

namespace DashboardService.Test
{
    [Collection("ElasticSearch in a container")]
    public class GetTotalSalesQueryTest
    {
        private readonly ElasticSearchInContainerFixture fixture;

        public GetTotalSalesQueryTest(ElasticSearchInContainerFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task TotalSales_All_Product_In_First_Q_2020()
        {
            var queryHandler = new GetTotalSalesHandler(new ElasticOrderRepository(fixture.ElasticClient()));

            var result = await queryHandler.Handle(
                new GetTotalSalesQuery
                {
                    SalesDateFrom = new DateTime(2020,1,1),
                    SalesDateTo = new DateTime(2020,3,31)
                }, 
                CancellationToken.None);

            result
                .Should()
                .HaveTotal(22, 1845M)
                .And
                .HaveProductTotal("Britania", 9, 640M)
                .And
                .HaveProductTotal("Tata", 6, 750M)
                .And
                .HaveProductTotal("Sunfeast", 7, 455M);
        }
        
        [Fact]
        public async Task TotalSales_HSI_Product_In_01_2020()
        {
            var queryHandler = new GetTotalSalesHandler(new ElasticOrderRepository(fixture.ElasticClient()));

            var result = await queryHandler.Handle(
                new GetTotalSalesQuery
                {
                    ProductCode = "Sunfeast",
                    SalesDateFrom = new DateTime(2020,1,1),
                    SalesDateTo = new DateTime(2020,1,31)
                }, 
                CancellationToken.None);
            
            result
                .Should()
                .HaveTotal(2, 150M)
                .And
                .HaveProductTotal("Sunfeast", 2, 150M);
        }
    }
}