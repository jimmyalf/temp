using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Data.Test.ContractSales.Factories
{
	public static class ArticleFactory
	{
		public static Article Get()
		{
			return new Article
			{
				Description = "Description",
				Name = "TestArticle",
				Number = "12345",
                DefaultSPCSAccountNumber = "3245"
			};
		}

		public static ContractArticleConnection GetContractArticleConnection(Article article, int contractCustomerId, float price, bool isVATFree)
		{
			return new ContractArticleConnection
			{
				Active = true,
				ArticleDescription = article.Description,
				ArticleId = article.Id,
				ArticleName = article.Name,
				ArticleNumber = article.Number,
				ContractCustomerId = contractCustomerId,
				EnableManualPriceOverride = false,
				NoVAT = isVATFree,
				Price = price,
				SPCSAccountNumber = "12345"
			};
		}
	}
}