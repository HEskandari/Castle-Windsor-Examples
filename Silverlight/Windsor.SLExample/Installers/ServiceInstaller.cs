using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Windsor.SLExample.Factories;
using Windsor.SLExample.Services;
using Windsor.SLExample.Services.Impl;

namespace Windsor.SLExample.Installers
{

    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Register using Service / Impl style using default lifestyle (singleton)
            //Each customer may override the default lifestyle using an attribute (see Customer Factory)
            container.AddFacility<TypedFactoryFacility>();
            container.Register(Component.For<ICustomerRepository>()
                                   .ImplementedBy<CustomerRepository>()
                                   .DependsOn(Property.ForKey("connectinString").Eq("Fake connection string for fake repository, but you get the idea")),
                               Component.For<IModelFactory>()
                                   .AsFactory());
        }
    }
}