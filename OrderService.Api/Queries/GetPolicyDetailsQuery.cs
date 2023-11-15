using MediatR;

namespace OrderService.Api.Queries
{
    public class GetOrderDetailsQuery : IRequest<GetOrderDetailsQueryResult>
    {
        public string OrderNumber { get; set; }    
    }
}