using System;
using System.Collections;
using System.Collections.Generic;
using FakeItEasy;
using log4net;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Synologen.LensSubscription.BGServiceCoordinator.App.Logging;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	[TestFixture]
	public abstract class CommonTaskTestBase : BehaviorTestBase<ITask>
	{
		protected ILog Log;
		protected IEventLoggingService EventLoggingService;
		protected IFileSectionToSendRepository FileSectionToSendRepository;
	    protected IReceivedFileRepository ReceivedFileRepository;
        protected IBGConfigurationSettingsService BgConfigurationSettingsService;
		protected TestTaskRepositoryResolver TaskRepositoryResolver;
		protected IAutogiroPayerRepository AutogiroPayerRepository;
		protected Log4NetLogger Log4NetLogger;

		protected override void SetUp()
		{
			Log = A.Fake<ILog>();
			EventLoggingService = A.Fake<IEventLoggingService>();
			Log4NetLogger = new Log4NetLogger(Log, EventLoggingService);
			FileSectionToSendRepository = A.Fake<IFileSectionToSendRepository>();
			ReceivedFileRepository = A.Fake<IReceivedFileRepository>();
			BgConfigurationSettingsService = A.Fake<IBGConfigurationSettingsService>();
			TaskRepositoryResolver = new TestTaskRepositoryResolver();
				//A.Fake<ITaskRepositoryResolver>();
			AutogiroPayerRepository = A.Fake<IAutogiroPayerRepository>();

			TaskRepositoryResolver.AddRepository(FileSectionToSendRepository);
			TaskRepositoryResolver.AddRepository(ReceivedFileRepository);
			TaskRepositoryResolver.AddRepository(AutogiroPayerRepository);
			//A.CallTo(() => TaskRepositoryResolver.GetRepository<IFileSectionToSendRepository>()).Returns(FileSectionToSendRepository);
			//A.CallTo(() => TaskRepositoryResolver.GetRepository<IReceivedFileRepository>()).Returns(ReceivedFileRepository);
			//A.CallTo(() => TaskRepositoryResolver.GetRepository<IAutogiroPayerRepository>()).Returns(AutogiroPayerRepository);
		}

		protected abstract ITask GetTask();

		protected ITask Task { get { return TestModel; } }
		protected override ITask GetTestModel() { return GetTask(); }
	}

	public class TestTaskRepositoryResolver : ITaskRepositoryResolver
	{
		private readonly Dictionary<string, object> _resolverItems;
		public TestTaskRepositoryResolver()
		{
			_resolverItems = new Dictionary<string, object>();
		}
		public TRepository GetRepository<TRepository>()
		{
			var key = typeof(TRepository).Name;
			if(_resolverItems.ContainsKey(key))
			{
				return (TRepository) _resolverItems[key];
			}
			throw new AssertionException(string.Format("No match for a repository of type {0} was found.", key));
		}

		public void AddRepository<TRepository>(TRepository repository)
		{
			var key = typeof(TRepository).Name;
			_resolverItems.Add(key, repository);
		}

	}
}