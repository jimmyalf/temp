using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class ContractCompanyMap : ClassMap<ContractCompany>
	{
		public ContractCompanyMap()
		{
			Table("tblSynologenCompany");
			Id(x => x.Id).Column("cId");
			Map(x => x.Name).Column("cName");
			Map(x => x.ContractId).Column("cContractCustomerId");
		}
	}
}