using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;

namespace Synologen.LensSubscription.ServiceCoordinator.Core.IoC
{
	public class TaskRepositoryResolver : ITaskRepositoryResolver
	{
		public virtual TRepository GetRepository<TRepository>()
		{
			return ObjectFactory.GetInstance<TRepository>();
		}
	}
}