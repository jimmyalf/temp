using System;
namespace Synologen.Service.Web.External.App.IoC
{
	public class ServiceHost : System.ServiceModel.ServiceHost
	{
		public ServiceHost() { }

		public ServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses) { }

		protected override void OnOpening()
		{
		    Description.Behaviors.Add(new ServiceBehavior());
		    base.OnOpening();
		}
	}
}