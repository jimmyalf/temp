using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class ContractArticleMap : ClassMap<ContractArticle>
	{
		public ContractArticleMap()
		{
			Table("tblSynologenContractArticleConnection");
			Id(x => x.Id).Column("cId");
			Map(x => x.ContractCustomerId).Column("cContractCustomerId");
			Map(x => x.IsVATFree).Column("cNoVat");
		}
	}
}