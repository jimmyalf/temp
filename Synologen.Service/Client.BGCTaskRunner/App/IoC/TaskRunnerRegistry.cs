using System;
using System.Reflection;
using EnterpriseDT.Net.Ftp;
using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.Autogiro.Readers;
using Synologen.LensSubscription.Autogiro.Writers;
using Synologen.LensSubscription.BGData;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.ServiceCoordinator.Core.IoC;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Synologen.Service.Client.BGCTaskRunner.App.Factories;
using Synologen.Service.Client.BGCTaskRunner.App.Logging;
using Synologen.Service.Client.BGCTaskRunner.App.Services;
using Consent = Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.Consent;
using ConsentsFile = Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.ConsentsFile;
using Payment = Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.Payment;
using PaymentsFile = Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.PaymentsFile;

namespace Synologen.Service.Client.BGCTaskRunner.App.IoC
{
	public class TaskRunnerRegistry : Registry
	{
		public readonly BGFtpServiceType CurrentServiceType;

		public TaskRunnerRegistry()
		{

			CurrentServiceType = GetServiceType();

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

			//Repositories
			For<IBGPaymentToSendRepository>().Use<BGPaymentToSendRepository>();
			For<IBGConsentToSendRepository>().Use<BGConsentToSendRepository>();
			For<IFileSectionToSendRepository>().Use<FileSectionToSendRepository>();
			For<IBGReceivedPaymentRepository>().Use<BGReceivedPaymentRepository>();
			For<IBGReceivedConsentRepository>().Use<BGReceivedConsentRepository>();
			For<IBGReceivedErrorRepository>().Use<BGReceivedErrorRepository>();
			For<IReceivedFileRepository>().Use<ReceivedFileRepository>();
			For<IAutogiroPayerRepository>().Use<AutogiroPayerRepository>();
			For<IBGFtpPasswordRepository>().Use<BGFtpPasswordRepository>();

			//Autogiro reader/writers/services
			For<IAutogiroFileWriter<PaymentsFile, Payment>>().Use<PaymentsFileWriter>();
			For<IAutogiroFileWriter<ConsentsFile, Consent>>().Use<ConsentsFileWriter>();
			For<IAutogiroFileReader<Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ConsentsFile, Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Consent>>().Use<ConsentsFileReader>();
			For<IAutogiroFileReader<ErrorsFile, Error>>().Use<ErrorFileReader>();
			For<IAutogiroFileReader<Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentsFile, Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Payment>>().Use<PaymentsFileReader>();
			For<IFileWriterService>().Use(x=> SendFileWriterServiceFactory.Get(x.GetInstance<IFileIOService>(), x.GetInstance<IBGServiceCoordinatorSettingsService>()));
			For<ITamperProtectedFileWriter>().Use(x => TamperProtectedFileWriterFactory.Get(x.GetInstance<IHashService>()));
			For<IHashService>().Use(x => HashServiceFactory.Get(x.GetInstance<IBGServiceCoordinatorSettingsService>()));
			For<IFtpService>().Use<BGFtpService>().Ctor<BGFtpServiceType>().Is(CurrentServiceType);
			For<IFtpIOService>().Use<BGFtpIOService>();
			For<IFileIOService>().Use<BGFileIOService>();
            For<IFileReaderService>().Use<BGReceivedFileReaderService>().Ctor<BGFtpServiceType>().Is(CurrentServiceType);
            For<IFileSplitter>().Use<ReceivedFileSplitter>();
			//For<IFtpCommandService>().Use<FtpCommandService>();
			For<IBGFtpPasswordService>().Use<BGFtpPasswordService>();
			For<IBGFtpChangePasswordService>().Use<BGFtpChangePasswordService>();

			//Settings
			For<IBGServiceCoordinatorSettingsService>().Use<BGServiceCoordinatorSettingsService>();

			For<FTPClient>().AlwaysUnique().Use(x => FTPClientFactory.GetClient(x.GetInstance<IBGServiceCoordinatorSettingsService>()));

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<NHibernateFactory>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
		}

		private static BGFtpServiceType GetServiceType()
		{
			switch (Mode.Current)
			{
				case RunningMode.InProduction: return BGFtpServiceType.Autogiro;
				case RunningMode.Test: return BGFtpServiceType.Autogiro_Test;
				case RunningMode.Debug: return BGFtpServiceType.Autogiro_Test;
				default: throw new ArgumentException("Cannot determine current taskrunner mode");
			}
		}

		protected virtual bool IsServiceCoordinatorTaskAssembly(Assembly assembly)
		{
			var assemblyName = assembly.GetName().Name;
			return assemblyName.StartsWith("Synologen.LensSubscription.BGServiceCoordinator.Task.");
		}
	}
}