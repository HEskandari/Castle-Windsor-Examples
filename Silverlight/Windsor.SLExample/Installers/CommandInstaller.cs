using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Windsor.SLExample.Installers
{
    public class CommandInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Register using CastleComponent attribute

            container.Register(AllTypes.FromThisAssembly()
                                       .Where(t => t.Namespace.EndsWith("Commands") && 
                                                   Component.IsCastleComponent(t)));
        }
    }
}