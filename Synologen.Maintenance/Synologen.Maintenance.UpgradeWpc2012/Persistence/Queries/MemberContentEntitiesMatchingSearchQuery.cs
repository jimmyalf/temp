using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class MemberContentEntitiesMatchingSearchQuery : PersistenceBase
	{
		private readonly string _query;

		public MemberContentEntitiesMatchingSearchQuery(string query)
		{
			_query = query;
		}

		public IEnumerable<MemberContentEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT cId, cMemberId, cBody FROM tblMembersContent")
				.Where("cBody LIKE @Match")
				.AddParameters(new { Match = '%' + EscapeSqlString(_query) + '%' });
			return Query(query, MemberContentEntity.Parse).ToList();
		}			 
	}
}