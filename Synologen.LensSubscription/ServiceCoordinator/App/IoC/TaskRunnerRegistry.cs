//#define DEBUG
using System.Reflection;
using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.ServiceCoordinator.App.Logging;
using Synologen.LensSubscription.ServiceCoordinator.Core.IoC;

namespace Synologen.LensSubscription.ServiceCoordinator.App.IoC
{
	public class TaskRunnerRegistry : Registry
	{
		public TaskRunnerRegistry()
		{
			// NHibernate
			For<IUnitOfWork>().LifecycleIs(new ExecutingTaskLifecycle()).Use<NHibernateUnitOfWork>();
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);
			For<ISubscriptionRepository>().Use<SubscriptionRepository>();
			For<ISubscriptionErrorRepository>().Use<SubscriptionErrorRepository>();
			For<ITransactionRepository>().Use<TransactionRepository>();

			// Logging
			For<ILoggingService>().Singleton().Use(LogFactory.CreateLoggingService());
	
			For<IBGWebServiceClient>().AlwaysUnique().Use<BgWebServiceClient>();
			For<IAutogiroPaymentService>().Use<AutogiroPaymentService>();
			For<IServiceCoordinatorSettingsService>().Use<ServiceCoordinatorSettingsService>(); 
			
			// Task scan
			Scan(x =>
			{
				x.AssembliesFromApplicationBaseDirectory(IsServiceCoordinatorTaskAssembly);
				x.AddAllTypesOf<ITask>();
			});

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<PageOfFramesMatchingCriteriaConverter>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
		}

		protected virtual bool IsServiceCoordinatorTaskAssembly(Assembly assembly)
		{
			var assemblyName = assembly.GetName().Name;
			return assemblyName.StartsWith("Synologen.LensSubscription.ServiceCoordinator.Task.");
		}
	}
}