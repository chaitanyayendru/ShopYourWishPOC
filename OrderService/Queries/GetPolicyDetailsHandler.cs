using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Queries;
using OrderService.Api.Queries.Dto;
using OrderService.Domain;

namespace OrderService.Queries
{
    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, GetOrderDetailsQueryResult>
    {
        private readonly IUnitOfWork uow;

        public GetOrderDetailsHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<GetOrderDetailsQueryResult> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await uow.Orders.WithNumber(request.OrderNumber);
            if (order == null)
            {
                throw new ApplicationException($"Order {request.OrderNumber} not found!");
            }
            
            return ConstructResult(order);
        }

        private GetOrderDetailsQueryResult ConstructResult(Order order)
        {
            var effectiveVersion = order.Versions.FirstVersion();
            
            return new GetOrderDetailsQueryResult
            {
                Order = new OrderDetailsDto
                {
                    Number = order.Number,
                    ProductCode = order.ProductCode,
                    DateFrom = effectiveVersion.CoverPeriod.ValidFrom,
                    DateTo = effectiveVersion.CoverPeriod.ValidTo,
                    OrderHolder = $"{effectiveVersion.OrderHolder.FirstName} {effectiveVersion.OrderHolder.LastName}",
                    TotalOrderValue = effectiveVersion.TotalOrderValueAmount,
                    
                    AccountNumber = null,
                    Covers = effectiveVersion.Covers.Select(c=>c.Code).ToList()
                }
            };
        }
    }
}