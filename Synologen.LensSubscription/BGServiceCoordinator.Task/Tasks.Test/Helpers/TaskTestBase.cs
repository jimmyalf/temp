using System;
using FakeItEasy;
using log4net;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Synologen.LensSubscription.BGServiceCoordinator.Logging;
using Consent=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.Consent;
using ConsentsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.ConsentsFile;
using Payment=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Payment;
using PaymentsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentsFile;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	[TestFixture]
	public abstract class TaskTestBase
	{
		protected ILog Log;
		protected IEventLoggingService EventLoggingService;
		protected IBGConsentToSendRepository BGConsentToSendRepository;
	    protected IBGReceivedConsentRepository BGReceivedConsentRepository;
	    protected IBGReceivedPaymentRepository BGReceivedPaymentRepository;
		protected IFileSectionToSendRepository FileSectionToSendRepository;
	    protected IReceivedFileRepository ReceivedFileRepository;
		protected IAutogiroFileWriter<ConsentsFile, Consent> ConsentFileWriter;
	    protected IAutogiroFileReader<PaymentsFile, Payment> PaymentFileReader;
        protected IAutogiroFileReader<Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ConsentsFile, Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Consent> ConsentFileReader;
		protected IBGPaymentToSendRepository BGPaymentToSendRepository;
        protected IBGConfigurationSettings BGConfigurationSettings;
		protected Log4NetLogger Log4NetLogger;

		protected TaskTestBase()
		{
			Log = A.Fake<ILog>();
			EventLoggingService = A.Fake<IEventLoggingService>();
			Log4NetLogger = new Log4NetLogger(Log, EventLoggingService);
			BGConsentToSendRepository = A.Fake<IBGConsentToSendRepository>();
		    BGReceivedConsentRepository = A.Fake<IBGReceivedConsentRepository>();
		    BGReceivedPaymentRepository = A.Fake<IBGReceivedPaymentRepository>();
            FileSectionToSendRepository = A.Fake<IFileSectionToSendRepository>();
		    ReceivedFileRepository = A.Fake<IReceivedFileRepository>();
			BGPaymentToSendRepository = A.Fake<IBGPaymentToSendRepository>();
			ConsentFileWriter = A.Fake<IAutogiroFileWriter<ConsentsFile, Consent>>();
            ConsentFileReader = A.Fake<IAutogiroFileReader<Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ConsentsFile, Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Consent>>();
            PaymentFileReader = A.Fake<IAutogiroFileReader<PaymentsFile, Payment>>();
            BGConfigurationSettings = A.Fake<IBGConfigurationSettings>();
			
			Context = () => { };
			Because = logger => { throw new AssertionException("An action for Because has not been set!"); };
		}

		[TestFixtureSetUp]
		protected void SetUpTest()
		{
			Context();
			Task = GetTask();
			Because(Task);
		}

		protected abstract ITask GetTask();

		protected ITask Task { get; private set; }

		protected Action Context;
		protected Action<ITask> Because;
	}
}