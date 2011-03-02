using System.Collections.Generic;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGTaskRunnerService : TaskRunnerService
	{
		public BGTaskRunnerService(ILoggingService loggingService, IEnumerable<ITask> tasks) 
			: base(loggingService, tasks) {}


		protected override void OnExecutedTask(ITask task)
		{
			var unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
			if(unitOfWork == null || unitOfWork.IsDisposed) return;
			try
			{
				unitOfWork.Commit();
			}
			catch
			{
				unitOfWork.Rollback();
			}
			finally
			{
				unitOfWork.Dispose();
			}
		}
	}
}