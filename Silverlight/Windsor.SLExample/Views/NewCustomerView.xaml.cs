using System.ComponentModel;
using Castle.Core;
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
