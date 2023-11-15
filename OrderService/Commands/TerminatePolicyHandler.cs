using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Commands;
using OrderService.Api.Events;
using OrderService.Domain;
using OrderService.Messaging;

namespace OrderService.Commands
{
    public class TerminateOrderHandler : IRequestHandler<TerminateOrderCommand, TerminateOrderResult>
    {
        private readonly IUnitOfWork uow;
        private readonly IEventPublisher eventPublisher;

        public TerminateOrderHandler(IUnitOfWork uow, IEventPublisher eventPublisher)
        {
            this.uow = uow;
            this.eventPublisher = eventPublisher;
        }

        public async Task<TerminateOrderResult> Handle(TerminateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await uow.Orders.WithNumber(request.OrderNumber);

            var terminationResult = order.Terminate(request.TerminationDate);
            
            await eventPublisher.PublishMessage(OrderTerminated(terminationResult));

            await uow.CommitChanges();
            
            return new TerminateOrderResult
            {
                OrderNumber = order.Number,
                MoneyToReturn = terminationResult.AmountToReturn
            };
        }

        private OrderTerminated OrderTerminated(OrderTerminationResult terminationResult)
        {
            return new OrderTerminated
            {
                OrderNumber = terminationResult.TerminalVersion.Order.Number,
                OrderFrom = terminationResult.TerminalVersion.CoverPeriod.ValidFrom,
                OrderTo = terminationResult.TerminalVersion.CoverPeriod.ValidTo,
                ProductCode = terminationResult.TerminalVersion.Order.ProductCode,
                TotalOrderValue = terminationResult.TerminalVersion.TotalOrderValueAmount,
                AmountToReturn = terminationResult.AmountToReturn,
                OrderHolder = new Api.Commands.Dtos.PersonDto
                {
                    FirstName = terminationResult.TerminalVersion.OrderHolder.FirstName,
                    LastName = terminationResult.TerminalVersion.OrderHolder.LastName,
                    TaxId = terminationResult.TerminalVersion.OrderHolder.Pesel
                }
            };
        }
    }
}