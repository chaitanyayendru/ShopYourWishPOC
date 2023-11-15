using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DashboardService.Api.Queries;
using DashboardService.Api.Queries.Dtos;
using DashboardService.Domain;
using MediatR;

namespace DashboardService.Queries
{
    public class GetSalesTrendsHandler : IRequestHandler<GetSalesTrendsQuery, GetSalesTrendsResult>
    {
        private readonly IOrderRepository orderRepository;

        public GetSalesTrendsHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }


        public Task<GetSalesTrendsResult> Handle(GetSalesTrendsQuery request, CancellationToken cancellationToken)
        {
            var queryResult = orderRepository.GetSalesTrend
            (
                new SalesTrendsQuery
                (
                    request.ProductCode,
                    request.SalesDateFrom,
                    request.SalesDateTo,
                    request.Unit.ToTimeAggregationUnit()
                )
            );

            return Task.FromResult(BuildResult(queryResult));
        }

        private GetSalesTrendsResult BuildResult(SalesTrendsResult queryResult)
        {
            var result = new GetSalesTrendsResult
            {
                PeriodsSales = new List<PeriodSaleDto>()
            };

            foreach (var periodSale in queryResult.PeriodSales)
            {
                result.PeriodsSales.Add(new PeriodSaleDto
                {
                    PeriodDate = periodSale.PeriodDate,
                    Period = periodSale.Period,
                    Sales = new SalesDto(periodSale.Sales.OrdersCount, periodSale.Sales.OrderValue)
                });
            }

            return result;

        }
    }
}