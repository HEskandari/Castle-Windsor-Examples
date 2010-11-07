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
	using System.Windows.Controls;
	using System.ComponentModel;

	using Castle.Core;
	using Castle.Samples.WindsorSilverlight.Commands;

	[Transient]
	public partial class AuthorizationView : INotifyPropertyChanged
	{
		private DeleteAllCommand deleteAllCommand;
		private LoginCommand loginCommand;
		private LogoutCommand logoutCommand;

		public AuthorizationView(
			DeleteAllCommand deleteCommand, 
			LoginCommand loginCommand, 
			LogoutCommand logoutCommand)
		{
			InitializeComponent();

			DataContext = this;

			DeleteAllCommand = deleteCommand;
			LogInCommand = loginCommand;
			LogoutCommand = logoutCommand;
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

		public LoginCommand LogInCommand
		{
			get { return loginCommand; }
			set
			{
				loginCommand = value;
				RaisePropertyChanged("LogInCommand");
			}
		}

		public LogoutCommand LogoutCommand
		{
			get { return logoutCommand; }
			set
			{
				logoutCommand = value;
				RaisePropertyChanged("LogoutCommand");
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
