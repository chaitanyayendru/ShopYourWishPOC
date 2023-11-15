using System.Linq;
using OrderService.Domain;
using Xunit;

namespace OrderService.Test.Domain
{
    public class OrderAssert
    {
        private readonly Order sut;

        private OrderAssert(Order sut)
        {
            this.sut = sut;
        }

        public static OrderAssert AssertThat(Order order)
        {
            return new OrderAssert(order);
        }

        public OrderAssert HasVersions(int expectedVersionsCount)
        {
            Assert.Equal(expectedVersionsCount, sut.Versions.Count);
            return this;
        }
        
        public OrderAssert HasVersion(int versionNumber)
        {
            Assert.NotNull(sut.Versions.FirstOrDefault(v => v.VersionNumber==versionNumber));
            return this;
        }

        public OrderAssert StatusIsActive()
        {
            Assert.Equal(OrderStatus.Active, sut.Status);
            return this;
        }
        
        public OrderAssert StatusIsTerminated()
        {
            Assert.Equal(OrderStatus.Terminated, sut.Status);
            return this;
        }
        
        public OrderAssert AgentIs(string agent)
        {
            Assert.Equal(agent, sut.AgentLogin);
            return this;
        }
    }


    public class OrderVersionAssert
    {
        private readonly OrderVersion sut;

        private OrderVersionAssert(OrderVersion sut)
        {
            this.sut = sut;
        }

        public static OrderVersionAssert AssertThat(OrderVersion orderVersion)
        {
            return new OrderVersionAssert(orderVersion);
        }
        
        public OrderVersionAssert TotalOrderValueIs(decimal expectedPremium)
        {
            Assert.Equal(expectedPremium, sut.TotalOrderValueAmount);
            return this;
        }
    }
}