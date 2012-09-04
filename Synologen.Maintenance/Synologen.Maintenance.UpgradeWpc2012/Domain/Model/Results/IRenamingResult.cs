namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public interface IRenamingResult
	{
		string OldPath { get; set; }
		string NewPath { get; set; }

		RenameEventArgs ToRenameEvent();
		string GetDescription();
	}

	public interface IRenamingResult<T> : IRenamingResult
	{
		T Id { get; set; }
	}
}