using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class ContractArticleFactory 
	{
		public static AddContractArticleView GetAddView(int contractId, int articleId)
		{
			return new AddContractArticleView
			{
				AllowCustomPricing = true,
				ContractId = contractId,
				IsActive = true,
				IsVATFreeArticle = true,
				PriceWithoutVAT = "456",
				ArticleId = articleId,
				SPCSAccountNumber = "9876"
			};
		}

		public static EditContractArticleView GetEditView(int contractArticleId)
		{
			return new EditContractArticleView
			{
				Id = contractArticleId,
				AllowCustomPricing = true,
				IsActive = true,
				IsVATFreeArticle = true,
				PriceWithoutVAT = "456",
				SPCSAccountNumber = "9876"
			};
		}

		public static ContractArticleConnection GetNew(int articleId, int contractId)
		{
			return new ContractArticleConnection
			{
				Id = 0,
				Active = false,
				ArticleId = articleId,
				ContractCustomerId = contractId,
				EnableManualPriceOverride = false,
				NoVAT = false,
				Price = 333.44f,
				SPCSAccountNumber = "9999"
			};
		}
	}
}