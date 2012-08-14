namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model
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
	}
}