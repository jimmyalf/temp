namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public interface ITask
	{
		void Execute(ExecutingTaskContext context);
		string TaskName { get; }
		int TaskOrder { get; }
	}
}