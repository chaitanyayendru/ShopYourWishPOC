using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OrderService.Domain
{
    public class OrderVersion
    {
        public virtual Guid? Id { get; protected set; }

        public virtual Order Order { get; protected set; }

        public virtual int VersionNumber { get; protected set; }

        public virtual OrderHolder OrderHolder { get; protected set; }

        public virtual ValidityPeriod CoverPeriod { get; protected set; }

        public virtual ValidityPeriod VersionValidityPeriod { get; protected set; }

        protected IList<OrderCover> covers = new List<OrderCover>();

        public virtual IReadOnlyCollection<OrderCover> Covers => new ReadOnlyCollection<OrderCover>(covers);

        public virtual decimal TotalOrderValueAmount { get; protected set; }

        protected OrderVersion() { } //NH

        public static OrderVersion FromOffer(
            Order order,
            int version,
            OrderHolder orderHolder,
            Offer offer)
        {
            return new OrderVersion(order,version,orderHolder,offer);
        }

        public virtual bool IsEffectiveOn(DateTime theDate)
        {
            return VersionValidityPeriod.Contains(theDate);
        }

        public virtual OrderVersion EndOn(DateTime endDate)
        {
            var endedCovers = this.covers.Select(c => c.EndOn(endDate)).ToList();
            
            var termVersion = new OrderVersion
            {
                Order = this.Order,
                VersionNumber = this.Order.NextVersionNumber(),
                OrderHolder = new OrderHolder(OrderHolder.FirstName, OrderHolder.LastName, OrderHolder.Pesel, OrderHolder.Address),
                CoverPeriod = CoverPeriod.EndOn(endDate),
                VersionValidityPeriod = ValidityPeriod.Between(endDate.AddDays(1), VersionValidityPeriod.ValidTo),
                covers = endedCovers,
                TotalOrderValueAmount = endedCovers.Sum(c => c.Premium)
            };
            return termVersion;
        }

        private OrderVersion(
            Order order,
            int version,
            OrderHolder orderHolder,
            Offer offer)
        {
            Order = order;
            VersionNumber = version;
            OrderHolder = orderHolder;
            CoverPeriod = offer.OrderValidityPeriod.Clone();
            VersionValidityPeriod = offer.OrderValidityPeriod.Clone();
            covers = offer.Covers.Select(c => new OrderCover(c, offer.OrderValidityPeriod.Clone())).ToList();
            TotalOrderValueAmount = covers.Sum(c => c.Premium);
        }
    }
}
