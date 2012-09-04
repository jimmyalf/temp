namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class FileEntityRenamingResult : IRenamingResult<int>
	{
		public int Id { get; set; }
		public string OldPath { get; set; }
		public string NewPath { get; set; }


		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldPath, NewPath, GetDescription());
		}

		public string GetDescription()
		{
			return string.Format("BaseFile[{0}] was renamed from {1} to {2}", Id, OldPath, NewPath);
		}
	}
}