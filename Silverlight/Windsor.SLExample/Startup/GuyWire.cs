using Castle.Windsor;
using Castle.Windsor.Installer;

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
    }
}