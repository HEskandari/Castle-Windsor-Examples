using System.ComponentModel;
using Castle.Core;
using Windsor.SLExample.Services;

namespace Windsor.SLExample.Views
{
    [Transient]
    public partial class CustomersView : INotifyPropertyChanged
    {
        public CustomersView(ICustomerRepository repository)
        {
            InitializeComponent();

            customers.ItemsSource = repository.GetAll();
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
