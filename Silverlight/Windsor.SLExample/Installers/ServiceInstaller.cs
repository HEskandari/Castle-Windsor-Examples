using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;
using Windsor.SLExample.Container;
using Windsor.SLExample.Factories;
using Windsor.SLExample.Services;
using Windsor.SLExample.Services.Impl;

namespace Windsor.SLExample.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var locator = new Adapter(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            //Register using Service / Impl style using default lifestyle (singleton)
            //Each customer may override the default lifestyle using an attribute (see Customer Factory)

            container.Register(Component.For<ICustomerRepository>()
                                        .ImplementedBy<CustomerRepository>())
                     .Register(Component.For<ICustomerFactory>()
                                        .ImplementedBy<CustomerFactory>())
                     .Register(Component.For<IWindsorContainer>()
                                        .Instance(container))
                     .Register(Component.For<IServiceLocator>()
                                        .Instance(locator));
        }
    }
}