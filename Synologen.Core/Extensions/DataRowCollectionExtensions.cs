using System.Collections.Generic;
using System.Data;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class DataRowCollectionExtensions
	{
		public static IEnumerable<DataRow> AsEnumerable(this DataRowCollection rows)
		{
			if(rows == null) yield break;
			foreach (DataRow row in rows)
			{
				yield return row;
			}
			yield break;
		}
	}
}