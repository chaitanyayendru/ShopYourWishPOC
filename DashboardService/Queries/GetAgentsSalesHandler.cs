using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DashboardService.Api.Queries;
using DashboardService.Api.Queries.Dtos;
using DashboardService.Domain;
using MediatR;

namespace DashboardService.Queries
{
    public class GetAgentsSalesHandler : IRequestHandler<GetAgentsSalesQuery, GetAgentsSalesResult>
    {
        private readonly IOrderRepository orderRepository;

        public GetAgentsSalesHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Task<GetAgentsSalesResult> Handle(GetAgentsSalesQuery request, CancellationToken cancellationToken)
        {
            var queryResult = orderRepository.GetAgentSales
            (
                new AgentSalesQuery
                (
                    request.AgentLogin,
                    request.ProductCode,
                    request.SalesDateFrom,
                    request.SalesDateTo
                )
            );

            return Task.FromResult(BuildResult(queryResult));
        }

        private GetAgentsSalesResult BuildResult(AgentSalesQueryResult queryResult)
        {
            var result  = new GetAgentsSalesResult
            {
                PerAgentTotal = new Dictionary<string, SalesDto>()
            };

            foreach (var agentResult in queryResult.PerAgentTotal)
            {
                result.PerAgentTotal[agentResult.Key] = new SalesDto
                {
                    OrdersCount = agentResult.Value.OrdersCount,
                    OrderAmount = agentResult.Value.OrderValue
                };
            }

            return result;
        }
    }
}