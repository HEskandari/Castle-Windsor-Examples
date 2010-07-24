using System;
using System.ComponentModel;
using System.Windows.Input;
using Castle.Core;
using Windsor.SLExample.Model;
using Windsor.SLExample.Services;

namespace Windsor.SLExample.Commands
{
    [CastleComponent(typeof(SaveCustomerCommand), Lifestyle = LifestyleType.Transient)]
    public class SaveCustomerCommand : ICommand
    {
        public ICustomerRepository Repository;

        public SaveCustomerCommand(ICustomerRepository repository)
        {
            Repository = repository;
        }

        public bool CanExecute(object parameter)
        {
            var c = parameter as Customer;
            return c != null;
        }

        public void Execute(object parameter)
        {
            var c = parameter as Customer;

            ((IEditableObject)c).EndEdit();

            Repository.Save(c);
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}