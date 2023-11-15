using System.Threading;
using System.Threading.Tasks;
using DashboardService.Domain;
using MediatR;
using OrderService.Api.Events;

namespace DashboardService.Listeners
{
    public class OrderCreatedHandler : INotificationHandler<OrderCreated>
    {
        private readonly IOrderRepository orderRepository;
        
        public OrderCreatedHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
        {
            var order = new OrderInfo
            (
                notification.OrderNumber, 
                notification.OrderFrom,
                notification.OrderTo,
                $"{notification.OrderHolder.FirstName} {notification.OrderHolder.LastName}",
                notification.ProductCode,
                notification.TotalOrderValue,
                notification.AgentLogin
            );

            orderRepository.Save(order);

            return Task.CompletedTask;
        }
    }
}
