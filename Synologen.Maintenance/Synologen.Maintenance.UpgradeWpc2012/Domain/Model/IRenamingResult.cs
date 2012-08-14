namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model
{
	public interface IRenamingResult
	{
		string OldPath { get; set; }
		string NewPath { get; set; }
	}
}