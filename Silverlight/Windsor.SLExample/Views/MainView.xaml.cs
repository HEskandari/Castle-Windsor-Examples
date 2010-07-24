using System.ComponentModel;
using Castle.Core;
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
            //Use pull model
            CurrentModel = _models.CreateModel<TModel>();
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
