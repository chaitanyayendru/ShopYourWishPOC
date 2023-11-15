using System;
using System.Collections.Generic;
using OrderService.Domain;

namespace OrderService.Test.Domain
{
    public static class OfferFactory
    {
        internal static Offer NewOfferValidUntilForAgent(DateTime offerValidityEnd,string agent)
        {
            using (var timeMachine = new TimeMachine(offerValidityEnd.AddDays(-30)))
            {
                var price = new Price(new Dictionary<string, decimal>()
                {
                    ["C1"] = 100M,
                    ["C2"] = 200M
                });

                var offer = Offer.ForPriceAndAgent
                (
                    "P1",
                    DateTime.Now,
                    DateTime.Now.AddDays(5),
                    OrderHolderFactory.Abc(),
                    price,
                    agent
                );

                return offer;
            }
        }
        
        internal static Offer NewOfferValidUntil(DateTime offerValidityEnd)
        {
            using (var timeMachine = new TimeMachine(offerValidityEnd.AddDays(-30)))
            {
                var price = new Price(new Dictionary<string, decimal>()
                {
                    ["C1"] = 100M,
                    ["C2"] = 200M
                });

                var offer = Offer.ForPrice
                (
                    "P1",
                    DateTime.Now,
                    DateTime.Now.AddDays(5),
                    OrderHolderFactory.Abc(),
                    price
                );

                return offer;
            }
        }

        internal static Offer AlreadyConvertedOffer()
        {
            var offer = NewOfferValidUntil(DateTime.Now.AddDays(5));
            offer.Buy(OrderHolderFactory.Abc());
            return offer;
        }
    }
}