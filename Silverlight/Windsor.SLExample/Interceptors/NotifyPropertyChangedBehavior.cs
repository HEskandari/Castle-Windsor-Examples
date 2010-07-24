using System;
using System.ComponentModel;
using System.Reflection;
using Castle.DynamicProxy;
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
                foreach (PropertyInfo prop in proxy.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite))
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
            if (methodsWithoutTarget.Contains(methodName))
                return false;
            return true;
        }
    }
}