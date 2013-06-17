namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public interface ITaskRepositoryResolver
	{
		TRepository GetRepository<TRepository>();
	}
}