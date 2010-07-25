// Copyright 2004-2009 Castle Project - http://www.castleproject.org/
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

using Castle.DynamicProxy;

using System;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace Windsor.SLExample.Interceptors
{
    public class NotifyPropertyChangedBehavior : IInterceptor
    {
        PropertyChangedEventHandler _handler;

        public void Intercept(IInvocation invocation)
        {
            string methodName = invocation.Method.Name;
            object[] arguments = invocation.Arguments;
            object proxy = invocation.Proxy;
            bool isEditableObject = proxy is IEditableObject;

            if (invocation.Method.DeclaringType.Equals(typeof(INotifyPropertyChanged)))
            {
                if (methodName == "add_PropertyChanged")
                {
                    StoreHandler((Delegate)arguments[0]);
                }
                if (methodName == "remove_PropertyChanged")
                {
                    RemoveHandler((Delegate)arguments[0]);
                }
            }

            if (!ShouldProceedWithInvocation(methodName))
                return;

            invocation.Proceed();

            NotifyPropertyChanged(methodName, proxy, isEditableObject);
        }

        protected void OnPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eventHandler = _handler;
            if (eventHandler != null) eventHandler(sender, e);
        }

        protected void RemoveHandler(Delegate @delegate)
        {
            _handler = (PropertyChangedEventHandler)Delegate.Remove(_handler, @delegate);
        }

        protected void StoreHandler(Delegate @delegate)
        {
            _handler = (PropertyChangedEventHandler)Delegate.Combine(_handler, @delegate);
        }

        protected void NotifyPropertyChanged(string methodName, object proxy, bool isEditableObject)
        {
            if ("CancelEdit".Equals(methodName) && isEditableObject)
            {
                var properties = proxy.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanWrite);

                foreach (var prop in properties)
                {
                    OnPropertyChanged(proxy, new PropertyChangedEventArgs(prop.Name));
                }
            }

            if (methodName.StartsWith("set_"))
            {
                string propertyName = methodName.Substring(4);

                var args = new PropertyChangedEventArgs(propertyName);
                OnPropertyChanged(proxy, args);
            }
        }

        protected bool ShouldProceedWithInvocation(string methodName)
        {
            var methodsWithoutTarget = new[] { "add_PropertyChanged", "remove_PropertyChanged" };
            return !methodsWithoutTarget.Contains(methodName);
        }
    }
}