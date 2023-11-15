using System;
using System.Threading.Tasks;
using NHibernate;
using OrderService.Domain;

namespace OrderService.DataAccess.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession session;
        private readonly ITransaction tx;
        private readonly OfferRepository offerRepository;
        private readonly OrderRepository orderRepository;

        public IOfferRepository Offers => offerRepository;

        public IOrderRepository Orders => orderRepository;


        public UnitOfWork(ISession session)
        {
            this.session = session;
            tx = session.BeginTransaction();
            offerRepository = new OfferRepository(session);
            orderRepository = new OrderRepository(session);
        }

        public async Task CommitChanges()
        {
            await tx.CommitAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                tx?.Dispose();
            }

        }
    }

    
}
