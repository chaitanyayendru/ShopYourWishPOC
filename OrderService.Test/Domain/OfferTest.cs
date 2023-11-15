using System;
using System.Collections.Generic;
using OrderService.Domain;
using Xunit;
using static Xunit.Assert;

namespace OrderService.Test.Domain
{
    public class OfferTest
    {
        [Fact]
        public void CanCreateOfferBasedOnPrice()
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
                null,
                price
            );

            OfferAssert
                .AssertThat(offer)
                .ProductCodeIs("P1")
                .StatusIsNew()
                .PriceIs(300M)
                .AgentIs(null);
        }
        
        [Fact]
        public void CanCreateOfferOnBehalfOfAgentBasedOnPrice()
        {
            var price = new Price(new Dictionary<string, decimal>
            {
                ["C1"] = 100M,
                ["C2"] = 200M
            });

            var offer = Offer.ForPriceAndAgent
            (
                "P1",
                DateTime.Now,
                DateTime.Now.AddDays(5),
                null,
                price,
                "rohit.son"
            );

            OfferAssert
                .AssertThat(offer)
                .ProductCodeIs("P1")
                .StatusIsNew()
                .PriceIs(300M)
                .AgentIs("rohit.son");
        }

        [Fact]
        public void CanBuyNewNonExpiredOffer()
        {
            var offer = OfferFactory.NewOfferValidUntil(DateTime.Now.AddDays(5));

            var order = offer.Buy(OrderHolderFactory.Abc());

            OfferAssert
                .AssertThat(offer)
                .StatusIsConverted();

            OrderAssert
                .AssertThat(order)
                .StatusIsActive()
                .HasVersions(1)
                .HasVersion(1)
                .AgentIs(null);

            OrderVersionAssert
                .AssertThat(order.Versions.WithNumber(1))
                .TotalOrderValueIs(offer.TotalPrice);
        }
        
        [Fact]
        public void CanBuyNewNonExpiredOfferFromAgent()
        {
            var offer = OfferFactory.NewOfferValidUntilForAgent(DateTime.Now.AddDays(5),"rohit.young");

            var order = offer.Buy(OrderHolderFactory.Abc());

            OfferAssert
                .AssertThat(offer)
                .StatusIsConverted()
                .AgentIs("rohit.young");

            OrderAssert
                .AssertThat(order)
                .StatusIsActive()
                .HasVersions(1)
                .HasVersion(1);

            OrderVersionAssert
                .AssertThat(order.Versions.WithNumber(1))
                .TotalOrderValueIs(offer.TotalPrice);
        }

        [Fact]
        public void CannotBuyAlreadyConvertedOffer()
        {
            var offer = OfferFactory.AlreadyConvertedOffer();

            Exception ex = Throws<ApplicationException>(() => offer.Buy(OrderHolderFactory.Abc()));
            Equal($"Offer {offer.Number} is not in new status and cannot be bought", ex.Message);
        }

        [Fact]
        public void CannotBuyExpiredOffer()
        {
            var offer = OfferFactory.NewOfferValidUntil(DateTime.Now.AddDays(-5));
            Exception ex = Throws<ApplicationException>(() => offer.Buy(OrderHolderFactory.Abc()));
            Equal($"Offer {offer.Number} has expired", ex.Message);
        }
    }
}