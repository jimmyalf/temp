using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.Autogiro.Helpers;

namespace Synologen.LensSubscription.Autogiro.Readers
{
	public abstract class BaseReader
	{
		public void SetupBase(string fileContents)
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