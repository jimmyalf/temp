using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Data.Queries
{
	public class All<TType> : Query<IEnumerable<TType>> where TType : class
	{
		public override IEnumerable<TType> Execute()
		{
			return Session.CreateCriteria<TType>().List<TType>();
		}
	}
}