using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories
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

		public static TransactionArticle Get(int id) 
		{
			return new TransactionArticle
			{
				Name = String.Concat("Artikel ", id.GetChar()),
				Active = (id % 3 == 0),
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

		public static IList<TransactionArticle> GetList(int numberOfItems) 
		{
			Func<int,TransactionArticle> converter = Get;
			return converter.GenerateRange(0,50).ToList();
		}
	}
}