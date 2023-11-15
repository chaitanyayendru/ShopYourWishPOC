using System;
using OrderService.Test.TestData;
using Xunit;
using static Xunit.Assert;

namespace OrderService.Test.Domain
{
    public class OrderTerminationTest
    {
        [Fact]
        public void CanTerminateActiveOrderInTheMiddleOfCoverPeriod()
        {
            var offer = OfferFactory.NewOfferValidUntil(DateTime.Now.AddDays(5));

            var order = offer.Buy(OrderHolderFactory.Abc());

            var terminationResult = order.Terminate(DateTime.Now.AddDays(3));

            OrderAssert
                .AssertThat(order)
                .HasVersions(2)
                .HasVersion(2)
                .StatusIsTerminated();

            OrderVersionAssert
                .AssertThat(terminationResult.TerminalVersion)
                .TotalOrderValueIs(180M);
        }

        [Fact]
        public void CannotTerminateTerminatedOrder()
        {
            var order = OrderFactory.AlreadyTerminatedOrder();

            Exception ex = Throws<ApplicationException>(() => order.Terminate(DateTime.Now));
            Equal($"Order {order.Number} is already terminated", ex.Message);
        }
    }
}