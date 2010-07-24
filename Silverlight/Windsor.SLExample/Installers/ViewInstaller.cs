using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Windsor.SLExample.Installers
{
    public class ViewInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Register all types and filter by predicate

            container.Register(AllTypes.FromThisAssembly()
                                       .Where(t => t.Namespace != null &&
                                                   t.Namespace.EndsWith("Views") &&
                                                   t.IsClass &&
                                                   t.IsInterface == false &&
                                                   t.IsAbstract == false));
        }
    }
}