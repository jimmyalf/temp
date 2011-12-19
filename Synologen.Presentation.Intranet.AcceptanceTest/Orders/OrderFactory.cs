using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	public static class OrderFactory
	{
		public static SaveCustomerEventArgs GetOrderCustomerForm(int? customerId = null)
		{
			return new SaveCustomerEventArgs
			{
				AddressLineOne = "Box 123",
				AddressLineTwo = "Datavägen 2",
				City = "Askim",
				Email = "adam.bertil@testbolaget.se",
				FirstName = "Adam",
				LastName = "Bertil",
				MobilePhone = "0701-123456",
				Notes = "Anteckningar ABC",
				PersonalIdNumber = "197001013239",
				Phone = "0317483000",
				PostalCode = "43632",
				CustomerId = customerId
			};
		}

		public static FetchCustomerDataByPersonalIdEventArgs GetPersonalIdForm()
		{
			return new FetchCustomerDataByPersonalIdEventArgs {PersonalIdNumber = "197001013239"};
		}

		public static SearchCustomerEventArgs GetSearchCustomerEventArgs(string personalIdNumber = "197001013239")
		{
			return new SearchCustomerEventArgs {PersonalIdNumber = personalIdNumber};
		}

		public static OrderCustomer GetCustomer(string personalIdNumber = "197001013239")
		{
			return new OrderCustomer
			{
				AddressLineOne = "Box 1234",
				AddressLineTwo = "Datavägen 23",
				City = "Mölndal",
				Email = "adam.b@testbolaget.se",
				FirstName = "Bertil",
				LastName = "Adamsson",
				MobilePhone = "0701-987654",
				Notes = "Anteckningar ABC DEF",
				PersonalIdNumber = personalIdNumber,
				Phone = "031123456",
				PostalCode = "41300",
			};
		}

	    public static CreateOrderEventArgs GetOrderEventArgs(int articleId=1)
	    {
	        return new CreateOrderEventArgs
	                   {
	                       ArticleId = articleId,
                           //CategoryId = 1,
                           LeftBaseCurve = 9,
                           LeftDiameter = -14,
                           LeftPower = 5,
                           LeftAxis = 5,
                           LeftCylinder = 5,
                           RightBaseCurve = 9,
                           RightDiameter = -14,
                           RightPower = 5,
                           RightAxis = 5,
                           RightCylinder = 5,
                           ShipmentOption = 1,
                           //SupplierId = 15,
                           //TypeId = 1
	                   };
	    }

		public static Order GetOrder(Article article, LensRecipe recipie = null)
		{
			return new Order
			{
				Article = article,
				LensRecipe = recipie,
				ShippingType = OrderShippingOption.ToCustomer,
			};
		}

	    public static Article GetArticle()
	    {
	        return new Article
	        {
	            Name = "Artikel 1",
	            Options = new ArticleOptions
	            {
	                Axis = new SequenceDefinition
	                {
	                    Increment = 0.25F,
                        Max = 2F,
                        Min = -1F
                    },
                    BaseCurve = new SequenceDefinition
                    {
                        Increment = 0.25F,
                        Max = 2F,
                        Min = -1F
                    },
                    Power = new SequenceDefinition
                    {
                        Increment = 0.25F,
                        Max = 2F,
                        Min = -1F
                    },
                    Cylinder = new SequenceDefinition
                    {
                        Increment = 0.25F,
                        Max = 2F,
                        Min = -1F
                    },
                    Diameter = new SequenceDefinition
                    {
                        Increment = 0.25F,
                        Max = 2F,
                        Min = -1F
                    }

                }
	        };
	    }

		public static Subscription GetSubscription(OrderCustomer customer)
		{
			return new Subscription
			{
				BankAccountNumber = "123456789",
				ClearingNumber = "1234",
				ActivatedDate = null,
				Active = false,
				ConsentStatus = SubscriptionConsentStatus.NotSent,
				Customer = customer,
			};
		}

        public static IEnumerable<ArticleCategory> GetCategories()
        {
            return Sequence.Generate(GetCategory, 15);
        }

        public static ArticleCategory GetCategory()
        {
            return new ArticleCategory { Name = "Linser" };
        }

	    public static IEnumerable<ArticleType> GetArticleTypes(ArticleCategory category)
	    {
	        return Sequence.Generate(() => GetArticleType(category), 4);
	    }

        public static ArticleType GetArticleType(ArticleCategory category)
        {
            return new ArticleType
            {
                Name = "Endagslinser",
                Category = category
            };
        }
	}
}