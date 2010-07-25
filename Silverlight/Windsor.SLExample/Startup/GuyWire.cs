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

namespace Windsor.SLExample.Startup
{
	using System.Windows;

	using Castle.Windsor;
	using Castle.Windsor.Installer;

	using Windsor.SLExample.Views;

	public class GuyWire
	{
		private IWindsorContainer _container;

		public GuyWire() : this(CreateContainer())
		{
		}

		public GuyWire(IWindsorContainer container)
		{
			_container = container;
		}

		private static IWindsorContainer CreateContainer()
		{
			return new WindsorContainer();
		}

		public void Wire()
		{
			_container.Install(FromAssembly.This());
		}

		public void Dewire()
		{
			if (_container != null)
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