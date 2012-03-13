using System.Collections.Generic;

namespace Synologen.Migration.AutoGiro2.Queries
{
	public class All<TType> : Query<IEnumerable<TType>> where TType : class
	{
		public override IEnumerable<TType> Execute()
		{
			return Session.CreateCriteria<TType>().List<TType>();
		}
	}
}