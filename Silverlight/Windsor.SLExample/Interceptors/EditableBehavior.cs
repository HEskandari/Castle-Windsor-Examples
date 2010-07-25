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

namespace Windsor.SLExample.Interceptors
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	using Castle.DynamicProxy;

	public class EditableBehavior : IInterceptor
	{
		private readonly IDictionary<PropertyInfo, object> tempValues = new Dictionary<PropertyInfo, object>();
		private bool isInEditMode;
		private Dictionary<string, PropertyInfo> properties;

		public virtual bool IsEditing
		{
			get { return isInEditMode; }
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
			     !invocation.Method.Name.StartsWith("set_")) || !IsEditing)
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
				//TODO: Enhance this.
				properties = new Dictionary<string, PropertyInfo>();
				foreach (var propertyInfo in propertyInfos)
				{
					if (!properties.ContainsKey(propertyInfo.Name))
						properties[propertyInfo.Name] = propertyInfo;
				}
			}

			var isSet = invocation.Method.Name.StartsWith("set_");
			var propertyName = invocation.Method.Name.Substring(4);
			PropertyInfo property;
			if (!properties.TryGetValue(propertyName, out property))
			{
				invocation.Proceed();
				return;
			}

			if (isSet)
			{
				tempValues[property] = invocation.Arguments[0];
			}
			else
			{
				invocation.Proceed();
				object value;
				if (tempValues.TryGetValue(property, out value))
					invocation.ReturnValue = value;
			}
		}

		#endregion

		public void BeginEdit()
		{
			isInEditMode = true;
		}

		public void CancelEdit()
		{
			tempValues.Clear();
			isInEditMode = false;
		}

		public void EndEdit(object target)
		{
			isInEditMode = false;

			foreach (var property in tempValues.Keys)
			{
				property.SetValue(target, tempValues[property], null);
			}

			tempValues.Clear();
		}
	}
}