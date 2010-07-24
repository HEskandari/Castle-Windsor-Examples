using Castle.Facilities.FactorySupport;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Windsor.SLExample.Installers
{
    public class ViewInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Register all types and filter by namespace
            container.Register(AllTypes.FromThisAssembly()
                                       .Where(Component.IsInNamespace("Windsor.SLExample.Views")));
        }
    }
}