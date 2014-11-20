using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class AllArticlesWithSupplierCriteria : IActionCriteria
	{
		public AllArticlesWithSupplierCriteria(int id)
		{
			SupplierId = id;
		}

		public int SupplierId { get; set; }
	}
}