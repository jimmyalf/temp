using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class ArticleFactory
	{
		public static ArticleView GetArticleView(int id)
		{
			return new ArticleView
			{
				ArticleNumber = "12345",
				DefaultSPCSAccountNumber = "3145",
				Description = "Bra artikel",
                Id = id,
                Name = "Article ABC"
			};
		}

		public static ArticleView GetArticleView()
		{
			return GetArticleView(0);
		}

		public static Article GetArticle()
		{
			return new Article
			{
				DefaultSPCSAccountNumber = "5755",
				Description = "Beskrivning",
				Id = 0,
				Name = "Namn",
				Number = "97645345"
			};
		}
	}
}