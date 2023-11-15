using System;
using OrderService.Domain;
using OrderService.Test.Domain;

namespace OrderService.Test.TestData
{
    public class OrderFactory
    {
        internal static Order AlreadyTerminatedOrder()
        {
            var offer = OfferFactory.NewOfferValidUntil(DateTime.Now.AddDays(5));

            var order = offer.Buy(OrderHolderFactory.Abc());

            order.Terminate(DateTime.Now.AddDays(3));

            return order;
        }
    }
}
