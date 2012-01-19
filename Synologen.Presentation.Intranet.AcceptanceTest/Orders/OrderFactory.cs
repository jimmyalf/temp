using System;
using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	public static class OrderFactory
	{
		public static SaveCustomerEventArgs GetOrderCustomerForm()
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
				//CustomerId = customerId
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

		public static OrderCustomer GetCustomer(Shop shop, string personalIdNumber = "197001013239")
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
				Shop = shop
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

		public static Order GetOrder(Shop shop, Article article, OrderCustomer customer, LensRecipe recipie = null, SubscriptionItem subscriptionItem = null)
		{
			return new Order
			{
				Article = article,
				LensRecipe = recipie,
				ShippingType = OrderShippingOption.ToCustomer,
				Customer = customer,
                SubscriptionPayment = subscriptionItem,
				Shop = shop,
                SelectedPaymentOption = new PaymentOption {Type = PaymentOptionType.Subscription_Autogiro_New}
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
                    },
                    Addition = new SequenceDefinition
                    {
                        Increment = 0F,
                        Max = 0F,
                        Min = 0F
                    }
                }
	        };
	    }

		public static Subscription GetSubscription(Shop shop, OrderCustomer customer, int seed = 0)
		{
			var active = seed % 3 != 0;
			return new Subscription
			{
				BankAccountNumber = "123456789",
				ClearingNumber = "1234",
				ActivatedDate = null,
				Active = active,
				ConsentStatus = SubscriptionConsentStatus.Accepted.SkipItems(seed),
				Customer = customer,
				Shop = shop
			};
		}

        public static IEnumerable<ArticleCategory> GetCategories()
        {
            return Sequence.Generate(GetCategory, 15);
        }
		public static IEnumerable<Subscription> GetSubscriptions(Shop shop, OrderCustomer customer)
		{
			return Sequence.Generate(seed => GetSubscription(shop, customer, seed), 15);
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

	    public static IEnumerable<Article> GetArticles(ArticleType articleType, ArticleSupplier supplier)
	    {
	        return Sequence.Generate(() => GetArticle(articleType, supplier), 10);
	    }

        public static IEnumerable<ListItem> FillWithIncrementalValues(SequenceDefinition sequence)
        {
            var list = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = (-9999).ToString() } };

            if (sequence.Increment > 0)
            {
                for (float value = sequence.Min; value <= sequence.Max; value += sequence.Increment)
                {
                    list.Add(new ListItem { Value = value.ToString(), Text = value.ToString() });
                }
            }
            return list;
        }

	    public static IEnumerable<ArticleSupplier> GetSuppliers()
	    {
	        return Sequence.Generate(GetSupplier, 5);
	    }

        public static ArticleSupplier GetSupplier()
        {
            return new ArticleSupplier
            {
                Name = "Johnsson & McBeth",
            };
        }

		public static AutogiroDetailsEventArgs GetAutogiroDetailsEventArgs()
		{
			return new AutogiroDetailsEventArgs
			{
				BankAccountNumber = "123456789",
				ClearingNumber = "1234",
				Description = "Betalning för endagslinser",
				Notes = "Ring kund",
				NumberOfPayments = 6,
				TaxFreeAmount = 100,
				TaxedAmount = 125.25M,
				AutoWithdrawalAmount = 900M
			};
		}

	    public static LensRecipe GetLensRecipe()
	    {
	        return new LensRecipe
	        {
	            Addition = new EyeParameter {Left = 1, Right = 1},
                Axis = new EyeParameter { Left = 2, Right = 2 },
                Power = new EyeParameter { Left = 3, Right = 3 },
                BaseCurve = new EyeParameter { Left = 4, Right = 4 },
                Diameter = new EyeParameter { Left = 5, Right = 5 },
                Cylinder = new EyeParameter { Left = 6, Right = 6 }
	        };
	    }

	    public static SubscriptionItem GetSubscriptionItem(Subscription subscription)
	    {
	        return new SubscriptionItem
	                   {
	                       Description = "desc",
                           Notes = "notes",
                           NumberOfPayments = 3,
                           NumberOfPaymentsLeft = 3,
                           Subscription = subscription,
                           TaxedAmount = 5000,
                           TaxFreeAmount = 4000
	                   };
	    }
	}
}