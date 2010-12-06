using System.Collections.Generic;
using System.Linq;
using Spinit.Wp.Synologen.Autogiro.Helpers;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro
{
	public abstract class BaseContentReader
	{
		protected BaseContentReader(string fileContents)
		{
			FileContents = fileContents;
			FileRows = fileContents.SplitIntoRows();
		}

		public string FileContents { get; private set; }

		public IList<string> FileRows { get; private set; }

		public string FirstRow { get { return FileRows.First(); } }

		public string LastRow { get { return FileRows.Last(); } }

		public IEnumerable<string> AllRowsButFirstAndLast
		{
			get { return FileRows.Except(ListExtensions.IgnoreType.FirstAndLast); }
		}
		public IEnumerable<string> AllRowsButFirst
		{
			get { return FileRows.Except(ListExtensions.IgnoreType.First); }
		}
	}
}