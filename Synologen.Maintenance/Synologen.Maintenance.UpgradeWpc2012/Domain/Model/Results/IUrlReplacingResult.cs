namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public interface IUrlReplacingResult
	{
		string OldUrl { get; }
		string NewUrl { get; }
		RenameEventArgs ToRenameEvent();
		string GetDescription();
	}
}