using System;
using System.Collections.Generic;

using Windsor.SLExample.Model;
using System.Linq;
using Windsor.SLExample.Factories;

namespace Windsor.SLExample.Services.Impl
{

    /// <summary>
    /// This should be a real service in a real application, 
    /// implemented by a WCF, REST or a SOAP based service.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string connectionString;
        private readonly IList<Customer> _customersData;

        public CustomerRepository(CustomerFactory factory, string connectionString)
        {
            this.connectionString = connectionString;
            _customersData = new List<Customer>
            {
                factory.Invoke("John", "Doe"),
                factory.Invoke("Jane", "Doe")
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