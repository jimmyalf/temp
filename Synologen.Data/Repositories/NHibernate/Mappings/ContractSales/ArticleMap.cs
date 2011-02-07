using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class ArticleMap : ClassMap<Article>
	{
		public ArticleMap()
		{
			Table("tblSynologenArticle");
			Id(x => x.Id).Column("cId");
			Map(x => x.Name).Column("cName");
			Map(x => x.Number).Column("cArticleNumber");
			//HasMany(x => x.ContractArticles)
			//    .KeyColumn("cArticleId")
			//    .Fetch.Join();
		}
	}
}