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

namespace Castle.Samples.WindsorSilverlight.Security
{
	using System.Security.Principal;
	using System.Collections.Generic;
	using System.Linq;

	public class AuthorizationManager : IAuthorizationManager, IAuthenticationManager
	{
		public IPrincipal CurrentPrincipal
		{
			get; set;
		}

		#region IAuthenticationManager

		public void Login(string username, string password, bool isSuperUser)
		{
			var roles = GetRoles(isSuperUser);

			CurrentPrincipal = new AppPrincipal(new AppIdentity(username, true), roles.ToArray());
		}

		public void Logout()
		{
			CurrentPrincipal = null;
		}

		#endregion

		#region IAuthorizationManager

		private IList<string> GetRoles(bool isSuperUser)
		{
			var roles = new List<string> { "Guest", "User" };
			
			if(isSuperUser)
				roles.Add("SuperUser");

			return roles;
		}

		#endregion
	}
}