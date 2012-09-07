namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public interface IEntityMigratedResult
	{
		string OldUrl { get; set; }
		string NewUrl { get; set; }
		RenameEventArgs ToRenameEvent();
	}
}