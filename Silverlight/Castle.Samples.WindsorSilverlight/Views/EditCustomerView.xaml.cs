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

using System.ComponentModel;
using System.Windows;
using Castle.Samples.WindsorSilverlight.Model;

namespace Castle.Samples.WindsorSilverlight.Views
{
	using Castle.Core;
	using Castle.Samples.WindsorSilverlight.Services;

	[Transient]
	public partial class EditCustomerView
	{
		public EditCustomerView(ICustomerRepository repository)
		{
			InitializeComponent();

			dg.ItemsSource = repository.GetAll();
		}

		private void ValidateCurrent(object sender, RoutedEventArgs e)
		{
			var customer = dg.SelectedItem as Customer;
			if (customer != null)
			{
				var validationError = ((IDataErrorInfo)customer).Error; //Customer implements IDataErrorInfo dynamically
				error.Text = validationError;
			}
		}
	}
}