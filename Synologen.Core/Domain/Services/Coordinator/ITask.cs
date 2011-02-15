namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public interface ITask
	{
		void Execute();
		string TaskName { get; }
		int TaskOrder { get; }
	}
}