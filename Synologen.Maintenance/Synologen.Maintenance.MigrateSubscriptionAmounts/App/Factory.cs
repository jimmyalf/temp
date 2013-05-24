using log4net;
using log4net.Config;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.App
{
	public static class Factory
	{
		 public static ILog CreateLogFor<TType>()
		 {
			var log = LogManager.GetLogger(typeof(TType));
			XmlConfigurator.Configure();
		 	return log;
		 }
	}
}