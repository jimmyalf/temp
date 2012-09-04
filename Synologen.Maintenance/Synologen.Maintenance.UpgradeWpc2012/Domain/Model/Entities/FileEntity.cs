using System.Data;
using System.IO;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities
{
	public class FileEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Directory { get; set; }
		public static FileEntity Parse(IDataRecord record)
		{
			return new FluentDataParser<FileEntity>(record)
				.Parse(x => x.Id)
				.Parse(x => x.Name)
				.Parse(x => x.Directory)
				.GetValue();
		}
		public virtual FileInfo GetFile(string rootDirectory)
		{
			var filePath = Name.Replace('/', '\\');
			var fullFilePath = Path.Combine(rootDirectory, filePath);
			return new FileInfo(fullFilePath);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}