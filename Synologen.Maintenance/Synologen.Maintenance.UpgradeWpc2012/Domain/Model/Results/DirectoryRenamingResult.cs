namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class DirectoryRenamingResult : IRenamingResult
	{
		public DirectoryRenamingResult(string oldPath, string newPath)
		{
			OldPath = oldPath;
			NewPath = newPath;
		}

		public string OldPath { get; set; }
		public string NewPath { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldPath, NewPath, GetDescription());
		}

		public string GetDescription()
		{
			return string.Format("Directory was renamed from {0} to {1}", OldPath, NewPath);
		}
	}
}