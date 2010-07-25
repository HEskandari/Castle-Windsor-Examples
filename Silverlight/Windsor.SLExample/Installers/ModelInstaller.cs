using System;
using System.ComponentModel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Windsor.SLExample.Interceptors;
using Windsor.SLExample.Model;

namespace Windsor.SLExample.Installers
{

    public class ModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Add interceptors and behaviors to the object
            container.Register(AllTypes.FromThisAssembly()
                                   .Where(Component.IsInSameNamespaceAs<Customer>())
                                   .Configure(c => c.LifeStyle.Transient
                                                       .Proxy.AdditionalInterfaces(typeof (IEditableObject),
                                                                                   typeof (INotifyPropertyChanged))
                                                       .Interceptors(typeof (EditableBehavior),
                                                                     typeof (NotifyPropertyChangedBehavior)))
                                   .ConfigureFor<Customer>(c => c.DynamicParameters(
                                       (k, @params) => @params.Insert(DateTime.Now))));
        }
    }
}