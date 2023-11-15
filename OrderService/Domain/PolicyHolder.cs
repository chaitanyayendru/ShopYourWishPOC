using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Domain
{
    public class OrderHolder
    {
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string Pesel { get; protected set; }
        public virtual Address Address { get; protected set; }

        public OrderHolder(string firstName, string lastName, string pesel, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            Address = address;
        }

        protected OrderHolder() { } //NH required
    }
}
