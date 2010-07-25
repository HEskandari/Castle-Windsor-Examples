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

using Castle.Core;
using System.ComponentModel;

using Windsor.SLExample.Commands;
using Windsor.SLExample.Model;

namespace Windsor.SLExample.Views
{
    [Transient]
    public partial class NewCustomerView : INotifyPropertyChanged
    {
        private SaveCustomerCommand _save;
        private Customer _customer;

        public NewCustomerView(Customer customer)
        {
            InitializeComponent();

            DataContext = this;

            CurrentCustomer = customer;
        }

        public Customer CurrentCustomer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                RaisePropertyChanged("CurrentCustomer");
            }
        }

        public SaveCustomerCommand Save
        {
            get { return _save; }
            set
            {
                _save = value;
                RaisePropertyChanged("Save");
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
