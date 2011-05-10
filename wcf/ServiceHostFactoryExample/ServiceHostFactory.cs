using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Spring.Aop.Framework.DynamicProxy;

namespace Shf
{
    public class ServiceHostFactory : System.ServiceModel.Activation.ServiceHostFactory
    {
        public class ShfCrisServiceHost : ServiceHost
        {
            public ShfCrisServiceHost(  object singletonInstance,
                                        params Uri[] baseAddresses)
                : base(singletonInstance, baseAddresses)
            {
            }

            protected override void ApplyConfiguration()
            {
                AdvisedProxy proxy = (AdvisedProxy)SingletonInstance;
                Description.Name = proxy.TargetType.Name;
                Description.ConfigurationName = proxy.TargetType.Name;
                base.ApplyConfiguration();
            }
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            object serviceSingleton = ServicesAopAroundAdvice.CreateService(serviceType);
            var serviceHost = new ShfCrisServiceHost(serviceSingleton, baseAddresses);

            serviceHost.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            serviceHost.AddServiceEndpoint(serviceType, binding, baseAddresses[0]);

            return serviceHost;
        }
    }
}
