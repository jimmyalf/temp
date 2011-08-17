using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Extensions;
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
                Name = "Article ABC",
				FormLegend = null
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

		public static IList<Core.Domain.Model.ContractSales.Article> GetArticles()
		{
			Func<int, Core.Domain.Model.ContractSales.Article> getArticle = GetDomainArticle;
			return getArticle.GenerateRange(1,15).ToList();
		}

		public static Core.Domain.Model.ContractSales.Article GetDomainArticle(int articleId)
		{
			var fakeObject = A.Fake<Core.Domain.Model.ContractSales.Article>();
			A.CallTo(() => fakeObject.Id).Returns(articleId);
			A.CallTo(() => fakeObject.Name).Returns("Article ABCDEF");
			A.CallTo(() => fakeObject.Number).Returns("54321");
			A.CallTo(() => fakeObject.SPCSAccountNumber).Returns("4513");
			return fakeObject;
		}
	}
}