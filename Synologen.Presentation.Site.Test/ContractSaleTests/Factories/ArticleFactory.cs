using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests.Factories
{
	public static class ArticleFactory
	{
		public static Article Get(int id, int contractId)
		{
			var mockedArticle = new Mock<Article>();
			mockedArticle.SetupGet(x => x.Id).Returns(id);
			mockedArticle.SetupGet(x => x.Name).Returns("Artikel " + id);
			mockedArticle.SetupGet(x => x.Number).Returns("123" + id);
			return mockedArticle.Object;
		}
	}
}