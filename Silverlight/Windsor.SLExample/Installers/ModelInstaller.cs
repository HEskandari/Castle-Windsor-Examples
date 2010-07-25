// Copyright 2004-2009 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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