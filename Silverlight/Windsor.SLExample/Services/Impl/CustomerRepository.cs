using System;
using System.Collections.Generic;
using Windsor.SLExample.Factories;
using Windsor.SLExample.Model;
using System.Linq;

namespace Windsor.SLExample.Services.Impl
{
    /// <summary>
    /// This should be a real service in a real application, 
    /// implemented by a WCF, REST or a SOAP based service.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IList<Customer> _customersData;

        public CustomerRepository(ICustomerFactory factory)
        {
            _customersData = new List<Customer>
            {
                factory.Create("John", "Doe"),
                factory.Create("Jane", "Doe")
            };
        }

        public Customer Find(Func<Customer, bool> predicate)
        {
            return GetAll().Where(predicate).FirstOrDefault();
        }

        public IList<Customer> GetAll()
        {
            return _customersData;
        }

        public void Save(Customer instance)
        {
            if(!_customersData.Contains(instance))
            {
                _customersData.Add(instance);
            }
        }
    }
}