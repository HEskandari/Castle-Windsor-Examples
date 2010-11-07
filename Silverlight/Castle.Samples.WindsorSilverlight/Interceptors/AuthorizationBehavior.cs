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

namespace Castle.Samples.WindsorSilverlight.Interceptors
{
	using System;

	using Castle.DynamicProxy;
	using Castle.Samples.WindsorSilverlight.Security;

	public class AuthorizationBehavior : IInterceptor
	{
		private readonly IAuthorizationManager authorizationManager;

		public AuthorizationBehavior(IAuthorizationManager authorizationManager)
		{
			this.authorizationManager = authorizationManager;
		}

		public void Intercept(IInvocation invocation)
		{
			var method = invocation.Method;
			var attributes = method.GetCustomAttributes(typeof(AuthorizeAttribute), true);

			if(attributes.Length == 0)
			{
				invocation.Proceed();
				return;
			}
			
			var authorization = attributes[0] as AuthorizeAttribute;
			if (authorization != null)
			{
				var requiredRole = authorization.Role;

				if(authorizationManager.CurrentPrincipal == null)
				{
					throw new UnauthorizedAccessException("You are not logged into the system.");
				}

				if(!string.IsNullOrEmpty(requiredRole) && !authorizationManager.CurrentPrincipal.IsInRole(requiredRole)) //Has requested role
				{
					throw new UnauthorizedAccessException(string.Format("Invalid access. You need {0} role for this operation.", requiredRole));
				}

				//Logged-in and has necessary roles, continue
				invocation.Proceed();
			}
		}
	}
}