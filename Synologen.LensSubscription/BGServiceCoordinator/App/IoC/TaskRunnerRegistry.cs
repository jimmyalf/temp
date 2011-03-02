using System.Reflection;
using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.Autogiro.Readers;
using Synologen.LensSubscription.Autogiro.Writers;
using Synologen.LensSubscription.BGData;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGServiceCoordinator.App.Factories;
using Synologen.LensSubscription.BGServiceCoordinator.App.Logging;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.TaskHelpers;
using Send=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Read=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;


namespace Synologen.LensSubscription.BGServiceCoordinator.App.IoC
{
	public class TaskRunnerRegistry : Registry
	{
		public TaskRunnerRegistry()
		{
			// NHibernate
			For<IUnitOfWork>().LifecycleIs(new ExecutingTaskLifecycle()).Use<NHibernateUnitOfWork>();
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);

			// Logging
			For<ILoggingService>().Singleton().Use(LogFactory.CreateLoggingService());
	
			// Task scan
			Scan(x =>
			{
				x.AssembliesFromApplicationBaseDirectory(IsServiceCoordinatorTaskAssembly);
				x.AddAllTypesOf<ITask>();
			});

			For<ITaskRepositoryResolver>().Use<TaskRepositoryResolver>();


			//Send Repositories
			For<IBGPaymentToSendRepository>().Use<BGPaymentToSendRepository>();
			For<IBGConsentToSendRepository>().Use<BGConsentToSendRepository>();
			For<IFileSectionToSendRepository>().Use<FileSectionToSendRepository>();

			//Recieve Repositories
			For<IBGReceivedPaymentRepository>().Use<BGReceivedPaymentRepository>();
			For<IBGReceivedConsentRepository>().Use<BGReceivedConsentRepository>();
			For<IBGReceivedErrorRepository>().Use<BGReceivedErrorRepository>();
			For<IReceivedFileRepository>().Use<ReceivedFileRepository>();

			//Autogiro reader/writers/services
			For<IAutogiroFileWriter<Send.PaymentsFile, Send.Payment>>().Use<PaymentsFileWriter>();
			For<IAutogiroFileWriter<Send.ConsentsFile, Send.Consent>>().Use<ConsentsFileWriter>();
			For<IAutogiroFileReader<Read.ConsentsFile, Read.Consent>>().Use<ConsentsFileReader>();
			For<IAutogiroFileReader<Read.ErrorsFile, Read.Error>>().Use<ErrorFileReader>();
			For<IAutogiroFileReader<Read.PaymentsFile, Read.Payment>>().Use<PaymentsFileReader>();
			For<IFileWriterService>().Use<BGSentFileWriterService>();
			For<ITamperProtectedFileWriter>().Use(x => TamperProtectedFileWriterFactory.Get(x.GetInstance<IHashService>()));
			For<IHashService>().Use(x => HashServiceFactory.Get(x.GetInstance<IBGConfigurationSettingsService>()));
			For<IFtpService>().Use<BGFtpService>().Ctor<BGFtpServiceType>().Is(BGFtpServiceType.Autogiro_Test);
			For<IFtpIOService>().Use<BGFtpIOService>();
			For<IFileIOService>().Use<BGFileIOService>();

			//Settings
			For<IBGConfigurationSettingsService>().Use<BGConfigurationSettingsService>();

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<NHibernateFactory>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
		}

		protected virtual bool IsServiceCoordinatorTaskAssembly(Assembly assembly)
		{
			var assemblyName = assembly.GetName().Name;
			return assemblyName.StartsWith("Synologen.LensSubscription.BGServiceCoordinator.Task.");
		}
	}
}