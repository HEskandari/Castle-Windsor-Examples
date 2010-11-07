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
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	using Castle.DynamicProxy;

	public class EditableBehavior : IInterceptor
	{
		private readonly IDictionary<PropertyInfo, object> edittedValues = new Dictionary<PropertyInfo, object>();
		private bool isEditing;
		private Dictionary<string, PropertyInfo> properties;

		public virtual bool IsEditing
		{
			get { return isEditing; }
		}

		#region IInterceptor Members

		public void Intercept(IInvocation invocation)
		{
			switch (invocation.Method.Name)
			{
				case "BeginEdit":
					BeginEdit();
					return;
				case "CancelEdit":
					CancelEdit();
					return;
				case "EndEdit":
					EndEdit(invocation.InvocationTarget ?? invocation.Proxy);
					return;
				default:
					break;
			}

			if ((!invocation.Method.Name.StartsWith("get_") &&
				 !invocation.Method.Name.StartsWith("set_")) || !IsEditing ||
				  invocation.InvocationTarget == null)
			{
				invocation.Proceed();
				return;
			}

			if (properties == null)
			{
				var propertyInfos = invocation.InvocationTarget
											  .GetType()
											  .GetProperties(BindingFlags.Public | BindingFlags.Instance)
											  .Where(p => p.CanWrite);

				properties = new Dictionary<string, PropertyInfo>();

				foreach (var propertyInfo in propertyInfos)
				{
					if (!properties.ContainsKey(propertyInfo.Name))
						properties.Add(propertyInfo.Name, propertyInfo);
				}
			}

			var isSetter = invocation.Method.Name.StartsWith("set_");
			var propertyName = invocation.Method.Name.Substring(4);

			PropertyInfo property;
			if (!properties.TryGetValue(propertyName, out property))
			{
				invocation.Proceed();
				return;
			}

			if (isSetter)
			{
				edittedValues[property] = invocation.Arguments[0];
			}
			else
			{
				invocation.Proceed();
				object value;

				if (edittedValues.TryGetValue(property, out value))
					invocation.ReturnValue = value;
			}
		}

		#endregion

		public void BeginEdit()
		{
			isEditing = true;
		}

		public void CancelEdit()
		{
			edittedValues.Clear();
			isEditing = false;
		}

		public void EndEdit(object target)
		{
			isEditing = false;

			foreach (var property in edittedValues.Keys)
			{
				property.SetValue(target, edittedValues[property], null);
			}

			edittedValues.Clear();
		}
	}
}