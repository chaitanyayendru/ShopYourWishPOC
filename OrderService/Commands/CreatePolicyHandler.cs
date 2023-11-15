using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Commands;
using OrderService.Api.Events;
using OrderService.Domain;
using OrderService.Messaging;

namespace OrderService.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IUnitOfWork uow;
        private readonly IEventPublisher eventPublisher;

        public CreateOrderHandler(
            IUnitOfWork uow,
            IEventPublisher eventPublisher)
        {
            this.uow = uow;
            this.eventPublisher = eventPublisher;
        }

        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            
            var offer = await uow.Offers.WithNumber(request.OfferNumber);
            var customer = new OrderHolder
            (
                request.OrderHolder.FirstName,
                request.OrderHolder.LastName,
                request.OrderHolder.TaxId,
                Address.Of
                (
                    request.OrderHolderAddress.Country,
                    request.OrderHolderAddress.ZipCode,
                    request.OrderHolderAddress.City,
                    request.OrderHolderAddress.Street
                )
            );
            var order = offer.Buy(customer);

            uow.Orders.Add(order);

            await eventPublisher.PublishMessage(OrderCreated(order)); 
            
            await uow.CommitChanges();

            return new CreateOrderResult
            {
                OrderNumber = order.Number
            };
            
        }

        private static OrderCreated OrderCreated(Order order)
        {
            var version = order.Versions.First(v => v.VersionNumber == 1);

            return new OrderCreated
            {
                OrderNumber = order.Number,
                OrderFrom = version.CoverPeriod.ValidFrom,
                OrderTo = version.CoverPeriod.ValidTo,
                ProductCode = order.ProductCode,
                TotalOrderValue = version.TotalOrderValueAmount,
                OrderHolder = new Api.Commands.Dtos.PersonDto
                {
                    FirstName = version.OrderHolder.FirstName,
                    LastName = version.OrderHolder.LastName,
                    TaxId = version.OrderHolder.Pesel
                },
                AgentLogin = order.AgentLogin
            };
        }
    }
}
