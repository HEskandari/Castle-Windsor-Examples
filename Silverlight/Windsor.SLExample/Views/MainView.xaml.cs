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

namespace Castle.Samples.WindsorSilverlight.Views
{
	using System.ComponentModel;

	using Castle.Core;
	using Castle.Samples.WindsorSilverlight.Commands;
	using Castle.Samples.WindsorSilverlight.Factories;

	[Singleton]
	public partial class MainView : INotifyPropertyChanged
	{
		private readonly IModelFactory models;
		private object currentModel;
		private ShowCommand<EditCustomerView> editCustomerCommand;
		private ShowCommand<NewCustomerView> newCustomerCommand;
		private ShowCommand<CustomersView> showCustomersCommand;

		public MainView(IModelFactory models)
		{
			InitializeComponent();

			DataContext = this;
			this.models = models;
		}

		public ShowCommand<CustomersView> ShowCustomers
		{
			get { return showCustomersCommand; }
			set
			{
				showCustomersCommand = value;
				RaisePropertyChanged("ShowCustomers");
			}
		}

		public ShowCommand<EditCustomerView> EditCustomers
		{
			get { return editCustomerCommand; }
			set
			{
				editCustomerCommand = value;
				RaisePropertyChanged("EditCustomers");
			}
		}

		public ShowCommand<NewCustomerView> NewCustomer
		{
			get { return newCustomerCommand; }
			set
			{
				newCustomerCommand = value;
				RaisePropertyChanged("NewCustomer");
			}
		}

		public object CurrentModel
		{
			get { return currentModel; }
			private set
			{
				var oldModel = currentModel;
				currentModel = value;
				models.FreeUpModel(oldModel);
				RaisePropertyChanged("CurrentModel");
			}
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		#endregion

		public void Show<TModel>()
		{
			CurrentModel = models.CreateModel<TModel>();
		}

		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}