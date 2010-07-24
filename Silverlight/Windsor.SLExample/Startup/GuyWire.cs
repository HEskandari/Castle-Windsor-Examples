using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Windows;
using Windsor.SLExample.Views;

namespace Windsor.SLExample.Startup
{

    public class GuyWire
    {
        private IWindsorContainer _container;

        public GuyWire() : this(CreateContainer())
        {
        }

        private static IWindsorContainer CreateContainer()
        {
            return new WindsorContainer();
        }

        public GuyWire(IWindsorContainer container)
        {
            _container = container;
        }

        public void Wire()
        {
            _container.Install(FromAssembly.This());
        }

        public void Dewire()
        {
            if(_container != null)
            {
                _container.Dispose();
            }

            _container = null;
        }

        public UIElement GetRoot()
        {
            return _container.Resolve<MainView>();
        }
    }
}