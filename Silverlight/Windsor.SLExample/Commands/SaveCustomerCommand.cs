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

namespace Windsor.SLExample.Commands
{
	using System;
	using System.ComponentModel;
	using System.Windows.Input;

	using Castle.Core;

	using Windsor.SLExample.Model;
	using Windsor.SLExample.Services;

	[CastleComponent(typeof (SaveCustomerCommand), Lifestyle = LifestyleType.Transient)]
	public class SaveCustomerCommand : ICommand
	{
		private readonly ICustomerRepository repository;

		public SaveCustomerCommand(ICustomerRepository repository)
		{
			this.repository = repository;
		}

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			var c = parameter as Customer;
			return c != null;
		}

		public void Execute(object parameter)
		{
			var c = parameter as Customer;

			((IEditableObject) c).EndEdit();

			repository.Save(c);
		}

		public event EventHandler CanExecuteChanged = delegate { };

		#endregion
	}
}