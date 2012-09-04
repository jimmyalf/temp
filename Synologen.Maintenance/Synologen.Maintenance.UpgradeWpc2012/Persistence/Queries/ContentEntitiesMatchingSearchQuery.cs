using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class ContentEntitiesMatchingSearchQuery : SearchQueryBase
	{
		public ContentEntitiesMatchingSearchQuery(string query) : base(query) { }

		public IList<ContentEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"
					SELECT 
						tblContPage.cId,
						tblContTree.cLocId,
						cUrl = dbo.sfContGetFileUrlDownString(tblContTree.cId),
						tblContPage.cName,
					FROM tblContPage 
					LEFT OUTER JOIN tblContTree ON tblContTree.cPgeId = tblContPage.cId")
				.Where("cContent LIKE @Match")
				.AddParameters(new { Match = '%' + ParsedQuery + '%' });

			return Query(query, ContentEntity.Parse).ToList();
		}		 
	}
}