using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class ContractArticleFactory 
	{
		public static ContractArticleView Get(int contractId, int selectedArticleId)
		{
			return new ContractArticleView
			{
				ContractId = contractId,
				SelectedArticleId = selectedArticleId,
				SPCSAccountNumber = "3512",
				AllowCustomPricing = true,
				Articles = null,
				IsActive = true,
				IsVATFreeArticle = true,
				PriceWithoutVAT = 555.55m
			};
		}

		public static ContractArticleView Get()
		{
			return Get(5, 7);
		}
	}
}