using System;
using System.Threading.Tasks;

namespace OrderService.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IOfferRepository Offers { get; }

        IOrderRepository Orders { get; }

        Task CommitChanges();
    }
    
}
