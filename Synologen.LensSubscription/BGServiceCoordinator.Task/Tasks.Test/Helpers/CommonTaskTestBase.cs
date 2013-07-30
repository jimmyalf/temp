using System.Collections.Generic;
using FakeItEasy;
using Spinit.Test;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class CommonTaskTestBase : BehaviorActionTestbase<ITask>
	{
		protected IFileSectionToSendRepository FileSectionToSendRepository;
	    protected IReceivedFileRepository ReceivedFileRepository;
        protected IBGServiceCoordinatorSettingsService BgServiceCoordinatorSettingsService;
		protected TestTaskRepositoryResolver TaskRepositoryResolver;
		protected IAutogiroPayerRepository AutogiroPayerRepository;
		protected FakeLoggingService LoggingService;

		protected override void SetUp()
		{
			LoggingService = new FakeLoggingService();
			FileSectionToSendRepository = A.Fake<IFileSectionToSendRepository>();
			ReceivedFileRepository = A.Fake<IReceivedFileRepository>();
			BgServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();
			AutogiroPayerRepository = A.Fake<IAutogiroPayerRepository>();
			TaskRepositoryResolver = new TestTaskRepositoryResolver();
			TaskRepositoryResolver.AddRepository(FileSectionToSendRepository);
			TaskRepositoryResolver.AddRepository(ReceivedFileRepository);
			TaskRepositoryResolver.AddRepository(AutogiroPayerRepository);
		}

		protected abstract ITask GetTask();

		protected ITask Task { get { return TestEntity; } }
		protected override ITask GetTestEntity()
		{
			return GetTask();
		}

		protected ExecutingTaskContext ExecutingTaskContext
		{
			get
			{
				return new ExecutingTaskContext(TestEntity, TaskRepositoryResolver);
			}
		}
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