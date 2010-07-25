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

using Windsor.SLExample.Factories;
using Windsor.SLExample.Commands;

namespace Windsor.SLExample.Views
{

    [Singleton]
    public partial class MainView : INotifyPropertyChanged
    {
        private object _currentModel;
        private ShowCommand<CustomersView> _showCustomersCommand;
        private ShowCommand<EditCustomerView> _editCustomerCommand;
        private ShowCommand<NewCustomerView> _newCustomerCommand;
        private readonly IModelFactory _models;

        public MainView(IModelFactory models)
        {
            InitializeComponent();

            DataContext = this;
            _models = models;
        }

        public ShowCommand<CustomersView> ShowCustomers
        {
            get { return _showCustomersCommand; }
            set
            {
                _showCustomersCommand = value;
                RaisePropertyChanged("ShowCustomers");
            }
        }

        public ShowCommand<EditCustomerView> EditCustomers
        {
            get { return _editCustomerCommand; }
            set
            {
                _editCustomerCommand = value;
                RaisePropertyChanged("EditCustomers");
            }
        }

        public ShowCommand<NewCustomerView> NewCustomer
        {
            get { return _newCustomerCommand; }
            set
            {
                _newCustomerCommand = value;
                RaisePropertyChanged("NewCustomer");
            }
        }

        public object CurrentModel
        {
            get { return _currentModel; }
            private set
            {
                var oldModel = _currentModel;
                _currentModel = value;
                _models.FreeUpModel(oldModel);
                RaisePropertyChanged("CurrentModel");
            }
        }

        public void Show<TModel>()
        {
            CurrentModel = _models.CreateModel<TModel>();
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
