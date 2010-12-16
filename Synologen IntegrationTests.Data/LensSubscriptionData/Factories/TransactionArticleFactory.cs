using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories
{
	public static class TransactionArticleFactory
	{
		public static TransactionArticle Get() 
		{
			return new TransactionArticle
			{
				Name = "Artikel A",
				Active = true,
			};
		}

		public static IList<TransactionArticle> GetList() 
		{
			return new List<TransactionArticle>
			{
				new TransactionArticle{ Name="Artikel A", Active = true },
				new TransactionArticle{ Name="Artikel B", Active = false },
				new TransactionArticle{ Name="Artikel C", Active = true },
				new TransactionArticle{ Name="Artikel D", Active = true },
				new TransactionArticle{ Name="Artikel E", Active = false },
				new TransactionArticle{ Name="Artikel F", Active = true },
			};
		}
	}
}