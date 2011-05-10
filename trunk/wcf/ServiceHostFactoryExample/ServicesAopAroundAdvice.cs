using System;
using System.Collections.Generic;
using AopAlliance.Intercept;
using Spring.Aop.Framework;

namespace Shf
{
    public class ServicesAopAroundAdvice : IMethodInterceptor
    {
        private static IDictionary<Type, ProxyFactory> _serviceFactories = new Dictionary<Type, ProxyFactory>();

        public object Invoke(IMethodInvocation invocation)
        {
            object returnValue = null;

            try
            {
                System.Diagnostics.Trace.WriteLine("before ...");
                returnValue = invocation.Proceed();
                System.Diagnostics.Trace.WriteLine("... after");
            }
            catch (Exception)
            {
                System.Diagnostics.Trace.WriteLine("Had an error");
                throw;
            }

            return returnValue;
        }

        private static ProxyFactory FindOrCreateProxyFactory(Type serviceInterfaceType, Type serviceType)
        {
            if (!_serviceFactories.ContainsKey(serviceInterfaceType))
                lock (typeof(ServicesAopAroundAdvice))
                    if (!_serviceFactories.ContainsKey(serviceInterfaceType))
                    {
                        ProxyFactory factory = new ProxyFactory(new Type[] { serviceInterfaceType });

                        object service = Activator.CreateInstance(serviceType);
                        factory.Target = service;
                        factory.AddAdvice(new ServicesAopAroundAdvice());

                        _serviceFactories.Add(serviceInterfaceType, factory);
                    }

            return _serviceFactories[serviceInterfaceType];
        }

        public static object CreateService(Type serviceInterfaceType)
        {
            string serviceTypeName = serviceInterfaceType.Name.Remove(0, 1);  // turn IMyService into MyService
            serviceTypeName = serviceInterfaceType.Namespace + "." + serviceTypeName;
            Type serviceType = Type.GetType(serviceTypeName, true);

            ProxyFactory factory = FindOrCreateProxyFactory(serviceInterfaceType, serviceType);
            return factory.GetProxy();
        }
    }
}
