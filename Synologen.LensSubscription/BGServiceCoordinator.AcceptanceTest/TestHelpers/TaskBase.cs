using System.Collections.Generic;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;
using Synologen.LensSubscription.BGData;
using Synologen.LensSubscription.ServiceCoordinator.Core.TaskRunner;
using Synologen.Test.Core;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers
{
	public abstract class TaskTestBase : BehaviorTestBase
	{
		protected IBGFtpPasswordService bGFtpPasswordService;
		protected IBGServiceCoordinatorSettingsService bgServiceCoordinatorSettingsService;
		protected IReceivedFileRepository receivedFileRepository;

		protected override void SetUp()
		{
			base.SetUp();
			bGFtpPasswordService = ResolveRepository<IBGFtpPasswordService>();
			bgServiceCoordinatorSettingsService = ResolveEntity<IBGServiceCoordinatorSettingsService>();
			receivedFileRepository = ResolveRepository<IReceivedFileRepository>();
		}

		protected TTask ResolveTask<TTask>() where TTask : ITask
		{
			return ObjectFactory.GetInstance<TTask>();
		}

		protected ITaskRunnerService GetTaskRunnerService(ITask task)
		{
			return ObjectFactory
				.With(typeof(IEnumerable<ITask>),new []{task})
				.With(typeof(ITaskRepositoryResolver), new TestTaskRepositoryResolver())
				.GetInstance<TaskRunnerService>();
		}


		private class TestTaskRepositoryResolver : ITaskRepositoryResolver
		{
			public TRepository GetRepository<TRepository>()
			{
				return ObjectFactory.GetInstance<TRepository>();
			}
		}

		protected TRepository ResolveRepository<TRepository>()
		{
			return ObjectFactory
				.With(typeof(ISession), GetBGSession())
				.GetInstance<TRepository>();
		}

		protected TEntity ResolveEntity<TEntity>()
		{
			return ObjectFactory.GetInstance<TEntity>();
		}

		protected static ISession GetBGSession()
		{
			return NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		}

		protected void StoreNewBGCFileOnDisk(string contents) 
		{ 
			var folderPath = bgServiceCoordinatorSettingsService.GetReceivedFilesFolderPath();
			var filePath = folderPath.TrimEnd('\\') + "\\UAGZZ.K0123456.D110329.T124300";
			if(System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
			}
			System.IO.File.WriteAllText(filePath, contents);
		}
	}
}