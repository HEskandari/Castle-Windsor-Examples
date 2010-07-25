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

namespace Castle.Samples.WindsorSilverlight.Services
{
	using System;
	using System.Collections.Generic;

	public interface IRepository<T> : IRepository where T : class
	{
		/// <summary>
		/// Finds one instance
		/// </summary>
		/// <returns></returns>
		T Find(Func<T, bool> predicate);

		/// <summary>
		/// Gets all instances
		/// </summary>
		/// <returns></returns>
		IList<T> GetAll();

		/// <summary>
		/// Saves the instance
		/// </summary>
		/// <param name="instance"></param>
		void Save(T instance);
	}

	public interface IRepository
	{
	}
}