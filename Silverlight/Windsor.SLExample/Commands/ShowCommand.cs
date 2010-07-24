using System;
using System.Windows;
using System.Windows.Input;
using Castle.Core;
using Windsor.SLExample.Views;

namespace Windsor.SLExample.Commands
{
    [CastleComponent(typeof(ShowCommand<>), Lifestyle = LifestyleType.Transient)]
    public class ShowCommand<T> : ICommand where T : UIElement
    {
        private readonly MainView _mainView;

        public ShowCommand(MainView mainView)
        {
            _mainView = mainView;
        }

        private void Show()
        {
            _mainView.Show<T>();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Show();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}