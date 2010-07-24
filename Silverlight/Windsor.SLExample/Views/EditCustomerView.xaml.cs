using Castle.Core;
using Windsor.SLExample.Services;

namespace Windsor.SLExample.Views
{
    [Transient]
    public partial class EditCustomerView
    {
        public EditCustomerView(ICustomerRepository repository)
        {
            InitializeComponent();

            dg.ItemsSource = repository.GetAll();
        }
    }
}
