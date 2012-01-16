using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class OrderFactory
	{
		public static Order GetOrder(Article article, OrderCustomer customer, LensRecipe recipie = null)
		{
			return new Order
			{
				Article = article,
				LensRecipe = recipie,
				ShippingType = OrderShippingOption.ToCustomer,
				Customer = customer
			};
		}
		
		public static Article GetArticle(ArticleType articleType, ArticleSupplier supplier)
	    {
	        return new Article
	        {
	            Name = "Artikel 1",
                ArticleType = articleType,
                ArticleSupplier = supplier,
	            Options = new ArticleOptions
	            {
	                Axis = new SequenceDefinition { Increment = 0.25F, Max = 2F, Min = -1F },
                    BaseCurve = new SequenceDefinition { Increment = 0.25F, Max = 2F, Min = -1F },
                    Power = new SequenceDefinition { Increment = 0.25F, Max = 2F, Min = -1F },
                    Cylinder = new SequenceDefinition { Increment = 0.25F, Max = 2F, Min = -1F },
                    Diameter = new SequenceDefinition { Increment = 0.25F, Max = 2F, Min = -1F },
                    Addition = new SequenceDefinition { Increment = 0F, Max = 0F, Min = 0F }
                }
	        };
	    }

		public static OrderCustomer GetCustomer(string personalIdNumber = "197001013239", string firstName = "Adam", string lastName = "Bertil")
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
			};
		}

		public static ArticleType GetArticleType(ArticleCategory category)
        {
            return new ArticleType
            {
                Name = "Endagslinser",
                Category = category
            };
        }

		public static ArticleSupplier GetSupplier()
        {
            return new ArticleSupplier
            {
                Name = "Johnsson & McBeth",
            };
        }

		public static ArticleCategory GetCategory()
        {
            return new ArticleCategory { Name = "Linser" };
        }

		public static IEnumerable<Order> GetOrders(Article article, OrderCustomer customer, LensRecipe recipie = null)
		{
			return Sequence.Generate(x => GetOrder(article, customer, recipie), 30);
		}
	}
}