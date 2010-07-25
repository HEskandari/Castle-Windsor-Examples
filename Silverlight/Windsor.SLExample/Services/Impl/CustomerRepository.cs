// Copyright 2004-2009 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Linq;
using System.Collections.Generic;

using Windsor.SLExample.Model;
using Windsor.SLExample.Factories;

namespace Windsor.SLExample.Services.Impl
{

    /// <summary>
    /// This should be a real service in a real application, 
    /// implemented by a WCF, REST or a SOAP based service.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _serviceUri;
        private readonly IList<Customer> _customersData;

        public CustomerRepository(CustomerFactory factory, string serviceUri)
        {
            _serviceUri = serviceUri; //The service Uri you'll use to fetch the data from.

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