using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
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
				ArticleId = new EyeParameter<int>(articleId, articleId),
				Addition = new EyeParameter<string>("Medium","High"),
				Axis = new EyeParameter<string>("25","33"),
				Power = new EyeParameter<string>("+3","-1"),
				BaseCurve = new EyeParameter<decimal?>(4,4),
				Diameter = new EyeParameter<decimal?>(5,5),
				Cylinder = new EyeParameter<string>("-10", "-22"),
				Quantity = new EyeParameter<string>("10 st", "12"),
				ShipmentOption = 1,
				Reference = "Text abc",
			};
	    }

		public static Order GetOrder(Shop shop, OrderCustomer customer, LensRecipe recipie = null, SubscriptionItem subscriptionItem = null, PaymentOptionType paymentOptionType = PaymentOptionType.Subscription_Autogiro_New)
		{
			return new Order
			{
				LensRecipe = recipie,
				ShippingType = OrderShippingOption.ToCustomer,
				Customer = customer,
                SubscriptionPayment = subscriptionItem,
				Shop = shop,
                SelectedPaymentOption = new PaymentOption {Type = paymentOptionType},
				OrderTotalWithdrawalAmount = 8000,
				Reference = "Referens-text"
			};
		}

	    public static Article GetArticle(ArticleType articleType, ArticleSupplier supplier, bool active = true)
	    {
	        return new Article
	        {
	            Name = "Artikel 1",
                ArticleType = articleType,
                ArticleSupplier = supplier,
	            Options = new ArticleOptions
	            {
	                EnableAxis = false,
                    BaseCurve = new SequenceDefinition(-1, 2, 0.25M),
                    Diameter = new SequenceDefinition(-1, 2, 0.25M),
                },
				Active = active
	        };
	    }

		public static Subscription GetSubscription(Shop shop, OrderCustomer customer, int seed = 0, bool? active = null, DateTime? consentedDate = null, SubscriptionConsentStatus? consentStatus = null)
		{
			var isActive = active ?? (seed % 3 == 0);
			var usedConsentStatus = consentStatus ?? SubscriptionConsentStatus.Accepted.SkipItems(seed);
			return new Subscription
			{
				BankAccountNumber = "123456789",
				ClearingNumber = "1234",
				ConsentedDate = consentedDate,
				Active = isActive,
				ConsentStatus = usedConsentStatus,
				Customer = customer,
				Shop = shop,
			};
		}

        public static IEnumerable<ArticleCategory> GetCategories()
        {
            return 
				Sequence.Generate(() => GetCategory(true), 15)
				.Concat(Sequence.Generate(() =>GetCategory(false),15));
        }
		public static IEnumerable<Subscription> GetSubscriptions(Shop shop, OrderCustomer customer)
		{
			return Sequence.Generate(seed => GetSubscription(shop, customer, seed), 15);
		}
        public static ArticleCategory GetCategory(bool active = true)
        {
            return new ArticleCategory { Name = "Linser" , Active = active};
        }

	    public static IEnumerable<ArticleType> GetArticleTypes(ArticleCategory category)
	    {
            return Sequence.Generate(() => GetArticleType(category,true), 4)
				.Concat(Sequence.Generate(() => GetArticleType(category, false), 4));
	    }

        public static ArticleType GetArticleType(ArticleCategory category, bool active = true)
        {
            return new ArticleType
            {
                Name = "Endagslinser",
                Category = category,
				Active = active
            };
        }

	    public static IEnumerable<Article> GetArticles(ArticleType articleType, ArticleSupplier supplier)
	    {
	        return 
				Sequence.Generate(() => GetArticle(articleType, supplier, true), 10)
				.Concat(Sequence.Generate(() => GetArticle(articleType, supplier, false), 10));
	    }

        public static IEnumerable<ListItem> FillWithIncrementalValues(SequenceDefinition sequence)
        {
			yield return new ListItem("-- Välj --","-9999");
        	foreach (var value in sequence.Enumerate())
        	{
        		yield return new ListItem {Value = value.ToString("N2"), Text = value.ToString("N2")};
        	}
        }

	    public static IEnumerable<ArticleSupplier> GetSuppliers()
	    {
	        return Sequence.Generate(() => GetSupplier(true), 5).Concat(Sequence.Generate(() => GetSupplier(false), 5));
	    }

        public static ArticleSupplier GetSupplier(bool active = true)
        {
            return new ArticleSupplier
            {
                Name = "Johnsson & McBeth",
                OrderEmailAddress = "erik.kinding@spinit.se",
				Active = active,
				ShippingOptions = OrderShippingOption.ToStore | OrderShippingOption.ToCustomer | OrderShippingOption.DeliveredInStore | OrderShippingOption.NoOrder
            };
        }

		public static AutogiroDetailsEventArgs GetAutogiroDetailsEventArgs()
		{
			return new AutogiroDetailsEventArgs
			{
				BankAccountNumber = "123456789",
				ClearingNumber = "1234",
				NumberOfPayments = 6,
				ProductPrice = 3500,
				FeePrice = 255,
			};
		}

	    public static LensRecipe GetLensRecipe(Article article)
	    {
	        return new LensRecipe
	        {
				Article = new EyeParameter<Article>(article, article),
	            Addition = new EyeParameter<string>("Medium","High"),
                Axis = new EyeParameter<string>("25","33"),
                Power = new EyeParameter<string>("+3","-1"),
                BaseCurve = new EyeParameter<decimal?>(4,4),
                Diameter = new EyeParameter<decimal?>(5,5),
                Cylinder = new EyeParameter<string>("-10", "-22"),
				Quantity = new EyeParameter<string>("10 st", "15")
	        };
	    }

	    public static SubscriptionItem GetSubscriptionItem(Subscription subscription)
	    {
	        return new SubscriptionItem
	        {
                WithdrawalsLimit = 3,
                PerformedWithdrawals = 0,
                Subscription = subscription,
                ProductPrice = 1000,
                FeePrice = 500
	        };
	    }

		public static IEnumerable<SubscriptionTransaction> GetTransactions(Subscription subscription)
		{
			Func<decimal, TransactionReason, TransactionType, SubscriptionTransaction> getTransaction = (amount, reason, type) => new SubscriptionTransaction
			{
				Amount = amount, 
				Reason = reason, 
				Subscription = subscription, 
				Type = type
			};
			var startTime = new DateTime(2011, 01, 01);
			return new[]
			{
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(1), () => getTransaction(1500, TransactionReason.Withdrawal, TransactionType.Withdrawal)),
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(2), () => getTransaction(1025.25m, TransactionReason.Correction, TransactionType.Deposit)),
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(3), () => getTransaction(999.99m, TransactionReason.Correction, TransactionType.Withdrawal)),
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(4), () => getTransaction(275, TransactionReason.PaymentFailed, TransactionType.Deposit)),
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(5), () => getTransaction(275, TransactionReason.Payment, TransactionType.Deposit)),
			};
		}

		public static IList<SubscriptionTransaction> GetTransactions(IEnumerable<Subscription> subscriptions)
		{
			return subscriptions.SelectMany(GetTransactions).ToList();
		}

		public static SubscriptionError GetError(Subscription subscription, DateTime? handledDate = null)
		{
			return new SubscriptionError
			{
				BGConsentId = 5,
				BGErrorId = 6,
				BGPaymentId = 7,
				Code = ConsentInformationCode.InitiatedByPayer,
				CreatedDate = new DateTime(2012, 02, 20),
				HandledDate = handledDate,
				Subscription = subscription,
				Type = SubscriptionErrorType.ConsentTemporarilyStoppedByPayer,
			};
		}

		public static IEnumerable<SubscriptionError> GetErrors(Subscription subscription)
		{
			Func<int,DateTime?> getDateOrNull = seed => seed % 3 == 0 ? new DateTime(2012, 02, 20) : (DateTime?) null;
			return Sequence.Generate(seed => GetError(subscription, getDateOrNull(seed)), 15);
		}
	}
}