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
	public interface IAuthenticationManager
	{
		/// <summary>
		/// Run authentication scheme and login the user.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="isSuperUser"></param>
		void Login(string username, string password, bool isSuperUser);

		/// <summary>
		/// Logouts the currently logged-in user
		/// </summary>
		void Logout();
	}
}