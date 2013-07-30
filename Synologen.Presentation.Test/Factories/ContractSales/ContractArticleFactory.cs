using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class ContractArticleFactory 
	{
		public static AddContractArticleView GetView(int contractId, int selectedArticleId)
		{
			return new AddContractArticleView
			{
				ContractId = contractId,
				ArticleId = selectedArticleId,
				SPCSAccountNumber = "3512",
				AllowCustomPricing = true,
				Articles = null,
				IsActive = true,
				IsVATFreeArticle = true,
				PriceWithoutVAT = "555"
			};
		}

		public static AddContractArticleView GetView(int contractId)
		{
			return GetView(contractId, 7);
		}

		public static ContractArticleConnection Get(int contractId, int selectedArticleId, int contractArticleId)
		{
			return new ContractArticleConnection
			{
				Active = true,
				ContractCustomerId = contractId,
				ArticleId = selectedArticleId,
				SPCSAccountNumber = "3512",
				EnableManualPriceOverride = true,
				NoVAT = true,
				Price = 555.55f,
				Id = contractArticleId
			};
		}

		public static ContractArticleConnection Get(int contractArticleId)
		{
			return Get(5, 7, contractArticleId);
		}

		public static ContractArticleConnection GetContractArticleRow(int contractId, int selectedArticleId, int contractArticleId)
		{
			return new ContractArticleConnection
			{
				Active = true,
				ContractCustomerId = contractId,
				ArticleId = selectedArticleId,
				SPCSAccountNumber = "3512",
				EnableManualPriceOverride = true,
				NoVAT = true,
				Price = 555.55f,
				Id = contractArticleId,
				ArticleName = "Artikel ABC"
			};
		}

		public static ContractArticleConnection GetContractArticleRow(int contractArticleId)
		{
			return GetContractArticleRow(5, 7, contractArticleId);
		}

		public static EditContractArticleView GetEditView(int id)
		{
			return new EditContractArticleView
			{
				Id = id,
				SPCSAccountNumber = "3512",
				AllowCustomPricing = true,
				IsActive = true,
				IsVATFreeArticle = true,
				PriceWithoutVAT = "555"
			};
		}
	}
}