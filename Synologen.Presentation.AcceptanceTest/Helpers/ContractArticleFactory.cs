using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class ContractArticleFactory 
	{
		public static ContractArticleView GetView(int contractId, int articleId)
		{
			return new ContractArticleView
			{
				AllowCustomPricing = true,
                Articles = null,
				ContractId = contractId,
				IsActive = true,
				IsVATFreeArticle = true,
				PriceWithoutVAT = 456.78m,
				SelectedArticleId = articleId,
				SPCSAccountNumber = "9876"
			};
		}
	}
}