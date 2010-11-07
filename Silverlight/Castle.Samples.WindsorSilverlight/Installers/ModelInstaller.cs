// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
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

namespace Castle.Samples.WindsorSilverlight.Installers
{
	using System;
	using System.ComponentModel;

	using Castle.MicroKernel.Registration;
	using Castle.MicroKernel.SubSystems.Configuration;
	using Castle.Samples.WindsorSilverlight.Interceptors;
	using Castle.Samples.WindsorSilverlight.Model;
	using Castle.Windsor;

	public class ModelInstaller : IWindsorInstaller
	{
		#region IWindsorInstaller Members

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			//Add interceptors and behaviors to the object
			container.Register(AllTypes.FromThisAssembly()
								.Where(Component.IsInSameNamespaceAs<Customer>())
								.Configure(c => c.LifeStyle.Transient
												 .Proxy.AdditionalInterfaces(typeof(IEditableObject),
																			 typeof(INotifyPropertyChanged),
																			 typeof(IDataErrorInfo))
												 .Interceptors(typeof(EditableBehavior),
															   typeof(NotifyPropertyChangedBehavior),
															   typeof(DataErrorInfoBehavior)))
								.ConfigureFor<Customer>(c => c.DynamicParameters(
									(k, @params) => @params.Insert(DateTime.Now))));
		}

		#endregion
	}
}