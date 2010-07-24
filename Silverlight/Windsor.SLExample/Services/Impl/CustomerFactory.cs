using System;
using Castle.Core;
using Castle.Windsor;
using Windsor.SLExample.Model;

namespace Windsor.SLExample.Services.Impl
{
    [Singleton]
    public class CustomerFactory : ICustomerFactory
    {
        private readonly IWindsorContainer _container;

        public CustomerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public Customer Create(string firstname, string lastname)
        {
            var c = _container.Resolve<Customer>();

            c.Firstname = firstname;
            c.Lastname = lastname;

            return c;
        }
    }
}