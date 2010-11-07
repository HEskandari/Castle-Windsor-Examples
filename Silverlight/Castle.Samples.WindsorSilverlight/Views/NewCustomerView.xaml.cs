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
	using Castle.Samples.WindsorSilverlight.Model;

	[Transient]
	public partial class NewCustomerView : INotifyPropertyChanged
	{
		private Customer customer;
		private SaveCustomerCommand saveCommand;
		private DeleteAllCommand deleteAllCommand;

		public NewCustomerView(Customer customer, SaveCustomerCommand saveCommand, DeleteAllCommand deleteCommand)
		{
			InitializeComponent();

			DataContext = this;

			CurrentCustomer = customer;
			SaveCommand = saveCommand;
			DeleteAllCommand = deleteCommand;
		}

		public Customer CurrentCustomer
		{
			get { return customer; }
			set
			{
				customer = value;
				RaisePropertyChanged("CurrentCustomer");
			}
		}

		public SaveCustomerCommand SaveCommand
		{
			get { return saveCommand; }
			set
			{
				saveCommand = value;
				RaisePropertyChanged("SaveCommand");
			}
		}

		public DeleteAllCommand DeleteAllCommand
		{
			get { return deleteAllCommand; }
			set
			{
				deleteAllCommand = value;
				RaisePropertyChanged("DeleteAllCommand");
			}
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		#endregion

		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}