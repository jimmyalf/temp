namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public interface ICoordinatorService
	{
		void Start();
		void Execute();
		void Stop();
	}
}