using System.Reflection;
using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.BGData;
using Synologen.LensSubscription.BGServiceCoordinator.Logging;

namespace Synologen.LensSubscription.BGServiceCoordinator.IoC
{
	public class TaskRunnerRegistry : Registry
	{
		public TaskRunnerRegistry()
		{
			// NHibernate
			For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<NHibernateUnitOfWork>();
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);

			// Logging
			For<ILoggingService>().Singleton().Use(LogFactory.CreateLoggingService());
	
			// Task scan
			Scan(x =>
			{
				x.Assembly(Assembly.GetExecutingAssembly());
				x.AddAllTypesOf<ITask>();
			});

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<NHibernateFactory>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
		}
	}
}