using System.ComponentModel;
using Castle.Core;
using Microsoft.Practices.ServiceLocation;
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

        public MainView()
        {
            InitializeComponent();

            DataContext = this;
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
            set
            {
                _currentModel = value;
                RaisePropertyChanged("CurrentModel");
            }
        }

        public void Show<TModel>()
        {
            //Use pull model
            CurrentModel = ServiceLocator.Current.GetInstance<TModel>();
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
