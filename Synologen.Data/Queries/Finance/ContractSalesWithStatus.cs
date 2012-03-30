using System.Collections.Generic;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Queries.Finance
{
	public class ContractSalesWithStatus : Query<IEnumerable<ContractSale>>
	{
		public int StatusId { get; set; }

		public ContractSalesWithStatus(int statusId)
		{
			StatusId = statusId;
		}

		public override IEnumerable<ContractSale> Execute()
		{
			return Session.CreateCriteria<ContractSale>()
				.Add(Restrictions.Eq("StatusId", StatusId))
				.List<ContractSale>();
		}
	}
}