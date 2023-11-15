using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DashboardService.Api.Queries;
using DashboardService.Api.Queries.Dtos;
using DashboardService.Domain;
using MediatR;

namespace DashboardService.Queries
{
    public class GetTotalSalesHandler : IRequestHandler<GetTotalSalesQuery,GetTotalSalesResult>
    {
        private readonly IOrderRepository orderRepository;

        public GetTotalSalesHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Task<GetTotalSalesResult> Handle(GetTotalSalesQuery request, CancellationToken cancellationToken)
        {
            var queryResult = orderRepository.GetTotalSales
            (
                new TotalSalesQuery
                (
                    request.ProductCode,
                    request.SalesDateFrom,
                    request.SalesDateTo
                )
            );

            return Task.FromResult(BuildResult(queryResult));
        }

        private GetTotalSalesResult BuildResult(TotalSalesQueryResult queryResult)
        {
            var result = new GetTotalSalesResult
            {
                Total = new SalesDto(queryResult.Total.OrdersCount, queryResult.Total.OrderValue),
                PerProductTotal = new Dictionary<string, SalesDto>()
            };

            foreach (var productTotal in queryResult.PerProductTotal)
            {
                result.PerProductTotal[productTotal.Key] = new SalesDto
                (
                    productTotal.Value.OrdersCount,
                    productTotal.Value.OrderValue
                );
            }

            return result;
        }
    }
}