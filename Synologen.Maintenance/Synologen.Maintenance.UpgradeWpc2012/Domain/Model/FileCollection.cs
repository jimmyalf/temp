using System.Collections;
using System.Collections.Generic;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model
{
	public class FileCollection : IEnumerable<FileEntry>
	{
		private readonly IList<FileEntry> _files;

		public FileCollection(IList<FileEntry> files)
		{
			_files = files;
		}

		public IEnumerator<FileEntry> GetEnumerator()
		{
			return _files.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count
		{
			get { return _files.Count; }
		}
	}
}