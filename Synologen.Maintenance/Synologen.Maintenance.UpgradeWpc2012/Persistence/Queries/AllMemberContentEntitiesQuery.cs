using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class AllMemberContentEntitiesQuery : PersistenceBase
	{
		public IList<MemberEntity> Execute()
		{
			var query = QueryBuilder.Build(@"SELECT cId, cMemberId, cBody FROM tblMembersContent");
			return Query(query, MemberEntity.Parse).ToList();
		}
	}
}