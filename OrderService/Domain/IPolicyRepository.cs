using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Domain
{
    public interface IOrderRepository
    {
        void Add(Order order);

        Task<Order> WithNumber(string number);
    }
}
