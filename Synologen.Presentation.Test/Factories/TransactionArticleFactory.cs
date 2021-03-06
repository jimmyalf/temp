using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories
{
	public static class TransactionArticleFactory 
	{
		public static IList<TransactionArticle> GetList()
		{
			Func<int, TransactionArticle> converter = Get;
			return converter.GenerateRange(1, 12).ToList();
		}

		public static TransactionArticle Get(int id)
		{
			var mockedArticle = new Mock<TransactionArticle>();
			mockedArticle.SetupGet(x => x.Id).Returns(id);
			mockedArticle.SetupGet(x => x.NumberOfConnectedTransactions).Returns((id % 3)*2);
			mockedArticle.SetupProperty(x => x.Name, "Artikel " + id);
			mockedArticle.SetupProperty(x => x.Active, false);
			return mockedArticle.Object;
		}

		public static TransactionArticleModel GetViewModel(int id) 
		{
			return new TransactionArticleModel
			{
				Active = true,
				Id = id,
				Name = "Artikel ABCD"
			};
		}
	}
}