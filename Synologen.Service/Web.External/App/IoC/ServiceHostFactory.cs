using System;

namespace Synologen.Service.Web.External.App.IoC
{
	public class ServiceHostFactory : System.ServiceModel.Activation.ServiceHostFactory
	{
		public ServiceHostFactory()
		{
			Bootstrapper.Boostrap();
		}


		protected override System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return new ServiceHost(serviceType, baseAddresses);
		}
	}
}