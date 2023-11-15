using NHibernate;
using OrderService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace OrderService.DataAccess.NHibernate
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ISession session;

        public OrderRepository(ISession session)
        {
            this.session = session;
        }

        public void Add(Order order)
        {
            session.Save(order);
        }

        public async Task<Order> WithNumber(string number)
        {
            return await session.Query<Order>().FirstOrDefaultAsync(p => p.Number == number);
        }
    }
}
