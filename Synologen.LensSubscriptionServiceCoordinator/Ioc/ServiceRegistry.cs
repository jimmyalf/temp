using System;
using System.Reflection;
using log4net;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Logging;
using StructureMap.Configuration.DSL;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Ioc
{
	public class ServiceRegistry : Registry
	{
		public ServiceRegistry()
		{
			For<ILog>().Use(Log4NetFactory.Create);
			For<IEventLoggingService>().Use(new EventLogLogger("TaskRunner"));
			For<ILoggingService>().Use<Log4NetLogger>();
			For<IBGWebService>().Use<TestBgWebServiceClient>();
			For<ISubscriptionRepository>().Use<TestSubscriptionRepository>();
			Scan(x =>
			{
				x.Assembly(Assembly.GetExecutingAssembly());
				x.AddAllTypesOf<ITask>();
			});
		}
	}
}