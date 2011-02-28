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
using Send_Consent=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.Consent;
using Send_Payment=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.Payment;
using Send_ConsentsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.ConsentsFile;
using Send_PaymentsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.PaymentsFile;
using Read_Payment=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Payment;
using Read_Error=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Error;
using Read_PaymentsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentsFile;
using Read_Consent=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Consent;
using Read_ConsentsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ConsentsFile;
using Read_ErrorsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ErrorsFile;


namespace Synologen.LensSubscription.BGServiceCoordinator.App.IoC
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
				x.AssembliesFromApplicationBaseDirectory(IsServiceCoordinatorTaskAssembly);
				x.AddAllTypesOf<ITask>();
			});


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
			For<IAutogiroFileWriter<Send_PaymentsFile, Send_Payment>>().Use<PaymentsFileWriter>();
			For<IAutogiroFileWriter<Send_ConsentsFile, Send_Consent>>().Use<ConsentsFileWriter>();
			For<IAutogiroFileReader<Read_ConsentsFile, Read_Consent>>().Use<ConsentsFileReader>();
			For<IAutogiroFileReader<Read_ErrorsFile, Read_Error>>().Use<ErrorFileReader>();
			For<IAutogiroFileReader<Read_PaymentsFile, Read_Payment>>().Use<PaymentsFileReader>();
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