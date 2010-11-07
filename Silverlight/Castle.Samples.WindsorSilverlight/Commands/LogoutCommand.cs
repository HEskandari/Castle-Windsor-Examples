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

namespace Castle.Samples.WindsorSilverlight.Commands
{
	using System;
	using System.Windows.Input;

	using Castle.Core;
	using Castle.Samples.WindsorSilverlight.Security;
	using Castle.Samples.WindsorSilverlight.Services;

	[CastleComponent(typeof(LogoutCommand), Lifestyle = LifestyleType.Transient)]
	public class LogoutCommand : ICommand
	{
		private readonly IAuthenticationManager authenticationManager;
		private readonly IStatusService statusService;

		public LogoutCommand(IAuthenticationManager authenticationManager, IStatusService statusService)
		{
			this.authenticationManager = authenticationManager;
			this.statusService = statusService;
		}

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			authenticationManager.Logout();
			statusService.ShowMessage("Logged-out successfully");
		}

		public event EventHandler CanExecuteChanged;

		#endregion
	}
}