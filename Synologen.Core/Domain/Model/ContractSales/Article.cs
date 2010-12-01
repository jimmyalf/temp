using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class Article : Entity
	{
		public Article() 
		{
			ContractArticles = Enumerable.Empty<ContractArticle>();
		}
		public virtual string Name { get; set; }
		public virtual string Number { get; set; }
		public virtual IEnumerable<ContractArticle> ContractArticles{ get; private set; }
		public virtual bool? IsVATFree(int contractId)
		{
			foreach (var contractArticle in ContractArticles)
			{
				if(contractArticle.ContractCustomerId.Equals(contractId))
				{
					return contractArticle.IsVATFree;
				}
			}
			return null;
		}
	}

	public class ContractArticle : Entity
	{
		public virtual int ContractCustomerId { get; set; }
		public virtual bool IsVATFree { get; set; }
	}
}