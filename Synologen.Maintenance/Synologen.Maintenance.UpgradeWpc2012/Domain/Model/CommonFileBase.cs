using System.IO;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model
{
	public abstract class CommonFileBase
	{
		protected readonly FileEntry FileEntry;

		internal CommonFileBase(FileEntry fileEntry)
		{
			FileEntry = fileEntry;
		}

		public string Name { get { return FileEntry.Name; }}
		public int Id { get { return FileEntry.Id; } }
		protected string GetPath(string root)
		{
			var filePath = Name.Replace('/', '\\');
			return Path.Combine(root, filePath);
		}
		public override string ToString() { return Name; }
	}
}