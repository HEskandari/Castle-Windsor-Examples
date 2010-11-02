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
	using System.Linq;
	using System.ComponentModel;
	
	using FluentValidation;

	using Castle.DynamicProxy;
	using Castle.Samples.WindsorSilverlight.Model;

	public class DataErrorInfoBehavior : IInterceptor
	{
		private IValidatorFactory factory;

		public DataErrorInfoBehavior(IValidatorFactory factory)
		{
			this.factory = factory;
		}

		public void Intercept(IInvocation invocation)
		{
			if (invocation.Method.DeclaringType.Equals(typeof(IDataErrorInfo)))
			{
				var validator = factory.GetValidator(typeof (Customer)); //This should be factory.GetValidator(invocation.GetUnderlyingTypeSomehow()); 
																		 //but still no good way to do this as it is not
																		 //possible to find the proxied object from
																		 //the IInvocation interface provided here. Could be a
																		 //Silverlight limitation in DP2 AFAIK.
				var result = validator.Validate(invocation.Proxy);

				if(result.IsValid) //object is valid
				{
					invocation.ReturnValue = string.Empty;
					return;
				}

				if (invocation.Method.Name == "get_Item") //Get the error for specific property
				{
					var propertyName = (string) invocation.Arguments[0]; //property name to validate

					var errors = result.Errors.Where(x => x.PropertyName.Contains(propertyName));
					invocation.ReturnValue = string.Join(Environment.NewLine, errors);
				}
				else if (invocation.Method.Name == "get_Error") //Get all errors of the object
				{
					var errors = result.Errors.Select(x => x.ErrorMessage);
					invocation.ReturnValue = string.Join(Environment.NewLine, errors);
				}
			}
			else
			{
				//Other methods not belonging to IDataErrorInfo,
				//so just invoke the method.
				invocation.Proceed();
			}
		}
		}
}