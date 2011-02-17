using System;
using FakeItEasy;
using log4net;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Synologen.LensSubscription.BGServiceCoordinator.Logging;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	[TestFixture]
	public abstract class TaskTestBase
	{
		protected ILog Log;
		protected IEventLoggingService EventLoggingService;
		protected IFileSectionToSendRepository FileSectionToSendRepository;
	    protected IReceivedFileRepository ReceivedFileRepository;
        protected IBGConfigurationSettingsService BgConfigurationSettingsService;
		protected Log4NetLogger Log4NetLogger;

		protected TaskTestBase()
		{
			Log = A.Fake<ILog>();
			EventLoggingService = A.Fake<IEventLoggingService>();
			Log4NetLogger = new Log4NetLogger(Log, EventLoggingService);
            FileSectionToSendRepository = A.Fake<IFileSectionToSendRepository>();
		    ReceivedFileRepository = A.Fake<IReceivedFileRepository>();
			BgConfigurationSettingsService = A.Fake<IBGConfigurationSettingsService>();
			
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