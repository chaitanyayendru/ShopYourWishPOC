using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrderService.Domain
{
    public enum OrderStatus
    {
        Active,
        Terminated
    }

    public class Order
    {
        public virtual Guid? Id { get; protected set; }

        public virtual string Number { get; protected set; }

        public virtual string ProductCode { get; protected set; }

        protected IList<OrderVersion> versions = new List<OrderVersion>();

        public virtual IReadOnlyCollection<OrderVersion> Versions => new ReadOnlyCollection<OrderVersion>(versions);

        public virtual OrderStatus Status { get; protected set; }

        public virtual DateTime CreationDate { get; protected set; }
        
        public virtual String AgentLogin { get; protected set; }

        protected Order() { } //NH constuctor

        public static Order FromOffer(OrderHolder orderHolder, Offer offer)
        {
            return new Order(orderHolder,offer);
        }
        
        protected Order(OrderHolder orderHolder, Offer offer)
        {
            Id = null;
            Number = Guid.NewGuid().ToString();
            ProductCode = offer.ProductCode;
            Status = OrderStatus.Active;
            CreationDate = SysTime.CurrentTime;
            AgentLogin = offer.AgentLogin;
            versions.Add(OrderVersion.FromOffer(this, 1, orderHolder, offer));
        }

        public virtual OrderTerminationResult Terminate(DateTime terminationDate)
        {
            //ensure is not already terminated
            if (Status != OrderStatus.Active)
                throw new ApplicationException($"Order {Number} is already terminated");

            //get version valid at term date
            var versionAtTerminationDate = versions.EffectiveOn(terminationDate);

            if (versionAtTerminationDate == null)
                throw new ApplicationException($"No valid order {Number} version exists at {terminationDate}. Order cannot be terminated.");

            if (!versionAtTerminationDate.CoverPeriod.Contains(terminationDate))
                throw new ApplicationException($"Order {Number} does not cover {terminationDate}. Order cannot be terminated at this date.");

            //create terminal version
            versions.Add(versionAtTerminationDate.EndOn(terminationDate));

            //change status
            Status = OrderStatus.Terminated;

            //return term version
            var terminalVersion = versions.LastVersion();
            return new OrderTerminationResult(terminalVersion, versionAtTerminationDate.TotalOrderValueAmount - terminalVersion.TotalOrderValueAmount);
        }

        public virtual int NextVersionNumber() => versions.Count == 0 ? 1 : versions.LastVersion().VersionNumber + 1;
    }
}
