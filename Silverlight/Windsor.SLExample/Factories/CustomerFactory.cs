using Castle.Core;
using System;
using Windsor.SLExample.Model;

namespace Windsor.SLExample.Factories
{

    [Singleton]
    public class CustomerFactory : ICustomerFactory
    {
        private readonly Func<Customer> _factory;

        public CustomerFactory(Func<Customer> factory)
        {
            _factory = factory;
        }

        public Customer Create(string firstname, string lastname)
        {
            var c = _factory.Invoke();

            c.Firstname = firstname;
            c.Lastname = lastname;

            return c;
        }
    }
}