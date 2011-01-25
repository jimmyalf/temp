using log4net.Core;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using StructureMap;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using Synologen.ServiceCoordinator.Logging;

namespace Synologen.ServiceCoordinator.Ioc
{
	public class ServiceRegistry : Registry
	{
		public ServiceRegistry()
		{
			For<ILoggingService>().Use<Log4NetLogger>();
		}
	}
}
