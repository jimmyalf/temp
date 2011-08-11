using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class ArticleFactory 
	{
		public static ArticleView GetArticleView()
		{
			return GetArticleView(0);
		}

		public static ArticleView GetArticleView(int articleId)
		{
			return new ArticleView
			{
				ArticleNumber = "12345",
				DefaultSPCSAccountNumber = "3145",
				Description = "Bra artikel",
                Id = articleId,
                Name = "Article ABC"
			};
		}

		public static Article GetArticle()
		{
			return GetArticle(5);
		}

		public static Article GetArticle(int articleId)
		{
			return new Article
			{
				Number = "54321",
				DefaultSPCSAccountNumber = "4513",
				Description = "Jättebra artikel",
                Id = articleId,
                Name = "Article ABCDEF"
			};
		}
	}
}