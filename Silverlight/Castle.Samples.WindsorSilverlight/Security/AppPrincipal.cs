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
	using System.Collections.Generic;
	using System.Security.Principal;

	public class AppPrincipal : IPrincipal
	{
		private readonly IIdentity identity;
		private readonly IList<string> roles;

		public AppPrincipal(IIdentity identity, string[] roles)
		{
			this.identity = identity;
			this.roles = roles;
		}

		public bool IsInRole(string role)
		{
			return roles.Contains(role);
		}

		public IIdentity Identity
		{
			get { return identity; }
		}
	}
}