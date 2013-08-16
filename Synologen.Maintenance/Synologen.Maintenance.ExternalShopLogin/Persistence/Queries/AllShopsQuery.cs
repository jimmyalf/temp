using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.ExternalShopLogin.Domain.Model;

namespace Synologen.Maintenance.ExternalShopLogin.Persistence.Queries
{
	public class AllShopsQuery : PersistenceBase
	{
		 public IEnumerable<Shop> Execute()
		 {
		 	var query = QueryBuilder
				.Build("SELECT * FROM tblSynologenShop")
				.Where("cExternalAccessUsername IS NULL");
		 	return Query(query, Shop.Parse);
		 }
	}
}