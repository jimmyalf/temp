using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class OrderFactory
	{
		public static Order GetOrder(Shop shop, OrderCustomer customer, LensRecipe recipie)
		{
			return new Order
			{
				LensRecipe = recipie,
				ShippingType = OrderShippingOption.ToCustomer,
				Customer = customer,
				Shop = shop
			};
		}

		public static LensRecipe GetLensRecipie(Article article)
		{
	        return new LensRecipe
	        {
				Article = new EyeParameter<Article>(article, article),
	            Addition = new EyeParameter<string>("Medium","High"),
                Axis = new EyeParameter<string>("25","33"),
                Power = new EyeParameter<string>("+3","-1"),
                BaseCurve = new EyeParameter<decimal?>(4,4),
                Diameter = new EyeParameter<decimal?>(5,5),
                Cylinder = new EyeParameter<string>("-10", "-22")
	        };
		}
		
		public static Article GetArticle(ArticleType articleType, ArticleSupplier supplier, string name = "Artikel", int? seed = null)
	    {
			var articleName = name + ((seed.HasValue) ? " " + seed.Value.GetSwedishChar() : string.Empty);
	        return new Article
	        {
	            Name = articleName,
                ArticleType = articleType,
                ArticleSupplier = supplier,
	            Options = GetArticleOptions()
	        };
	    }

		public static ArticleOptions GetArticleOptions()
		{
			return new ArticleOptions
			{
				BaseCurve = new SequenceDefinition { Increment = 0.25M, Max = 2M, Min = -1M },
				Diameter = new SequenceDefinition { Increment = 0.25M, Max = 2M, Min = -1M },
			};
		}

		public static IEnumerable<Article> GetArticles(ArticleType articleType, ArticleSupplier supplier)
		{
			return Sequence.Generate(seed => GetArticle(articleType, supplier, seed: seed), 45);
		}

		public static OrderCustomer GetCustomer(Shop shop, string personalIdNumber = "197001013239", string firstName = "Adam", string lastName = "Bertil")
		{
			return new OrderCustomer
			{
				AddressLineOne = "Box 1234",
				AddressLineTwo = "Datavägen 23",
				City = "Mölndal",
				Email = "adam.b@testbolaget.se",
				FirstName = firstName,
				LastName = lastName,
				MobilePhone = "0701-987654",
				Notes = "Anteckningar ABC DEF",
				PersonalIdNumber = personalIdNumber,
				Phone = "031123456",
				PostalCode = "41300",
				Shop = shop
			};
		}

		public static ArticleType GetArticleType(ArticleCategory category, int? seed = null, string name = "Endagslinser")
        {
			var articleTypeName = name + ((seed.HasValue) ? " " + seed.Value.GetSwedishChar() : string.Empty);
            return new ArticleType { Name = articleTypeName, Category = category };
        }

		public static IEnumerable<ArticleType> GetArticleTypes(ArticleCategory category)
		{
			return Sequence.Generate(seed => GetArticleType(category, seed), 45);
		}

		public static ArticleSupplier GetSupplier(int? seed = null, string name = "Johnsson & McBeth")
		{
			var supplierName = name + ((seed.HasValue) ? " " + seed.Value.GetSwedishChar() : string.Empty);
            return new ArticleSupplier { Name = supplierName, OrderEmailAddress = "info@spinit.se" };
        }

		public static ArticleCategory GetCategory(string name = "Linser", int? seed = null)
		{
			var categoryName = name + ((seed.HasValue) ? " " + seed.Value.GetSwedishChar() : string.Empty);
            return new ArticleCategory { Name = categoryName };
        }

		public static IEnumerable<ArticleCategory> GetCategories()
		{
			return Sequence.Generate(seed => GetCategory(seed: seed), 45);
		}

		public static IEnumerable<Order> GetOrders(Shop shop, OrderCustomer customer, LensRecipe recipie)
		{
			return Sequence.Generate(x => GetOrder(shop, customer, recipie), 30);
		}

		public static IEnumerable<ArticleSupplier> GetSuppliers()
		{
			return Sequence.Generate(seed => GetSupplier(seed), 45);
		}
	}
}