using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Enumerations;
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
				AddressLineTwo = "Datav�gen 2",
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
				AddressLineTwo = "Datav�gen 23",
				City = "M�lndal",
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

	    public static OrderChangedEventArgs GetOrderEventArgs(int articleId, int categoryId, int supplierId, int articletypeId)
	    {
	        return new OrderChangedEventArgs
			{
				SelectedCategoryId = categoryId,
				SelectedArticleTypeId = articletypeId,
				SelectedSupplierId = supplierId,
				SelectedArticleId = new EyeParameter<int?>(articleId, articleId),
				SelectedAddition = new EyeParameter<string>("Medium","High"),
				SelectedAxis = new EyeParameter<string>("25","33"),
				SelectedPower = new EyeParameter<string>("+3","-1"),
				SelectedBaseCurve = new EyeParameter<decimal?>(4,4),
				SelectedDiameter = new EyeParameter<decimal?>(5,5),
				SelectedCylinder = new EyeParameter<string>("-10", "-22"),
				SelectedQuantity = new EyeParameter<string>("10 st", "12"),
				SelectedShippingOption = 1,
				SelectedReference = "Text abc",
				OnlyUse = new EyeParameter<bool>()
			};
	    }

		public static OrderChangedEventArgs GetOrderEventArgsLeftEyeOnly(int articleId, int categoryId, int supplierId, int articletypeId)
		{
	        return new OrderChangedEventArgs
			{
				SelectedCategoryId = categoryId,
				SelectedArticleTypeId = articletypeId,
				SelectedSupplierId = supplierId,
				SelectedArticleId = new EyeParameter<int?>(articleId, null),
				SelectedAddition = new EyeParameter<string>("Medium",null),
				SelectedAxis = new EyeParameter<string>("25",null),
				SelectedPower = new EyeParameter<string>("+3",null),
				SelectedBaseCurve = new EyeParameter<decimal?>(4,null),
				SelectedDiameter = new EyeParameter<decimal?>(5,null),
				SelectedCylinder = new EyeParameter<string>("-10", null),
				SelectedQuantity = new EyeParameter<string>("10 st", null),
				SelectedShippingOption = 1,
				SelectedReference = "Text abc",
				OnlyUse = new EyeParameter<bool>(true, false)
			};
		}

		public static Order GetOrder(Shop shop, OrderCustomer customer, LensRecipe recipie = null, SubscriptionItem subscriptionItem = null, PaymentOptionType paymentOptionType = PaymentOptionType.Subscription_Autogiro_New)
		{
			var order = new Order
			{
				LensRecipe = recipie,
				ShippingType = OrderShippingOption.ToCustomer,
				Customer = customer,
                SubscriptionPayment = subscriptionItem,
				Shop = shop,
                SelectedPaymentOption = new PaymentOption
                {
                	Type = paymentOptionType, 
					SubscriptionId = (subscriptionItem == null) ? (int?) null : subscriptionItem.Subscription.Id
                },
				Reference = "Referens-text"
			};
			order.SetWithdrawalAmount(new SubscriptionAmount(8000,1000));
			return order;
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
			yield return new ListItem("-- V�lj --","-9999");
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

		public static AutogiroDetailsEventArgs GetAutogiroDetailsEventArgs(bool isOngoing = false)
		{
		    if (isOngoing)
		    {
		        return new AutogiroDetailsEventArgs
				{
					BankAccountNumber = "123456789",
					ClearingNumber = "1234",
					Type = SubscriptionType.Ongoing,
					ProductPrice = 3500,
					FeePrice = 255,
					MonthlyFee = 25,
					MonthlyProduct = 175,
                    Title = "Testabonnemang"
				};				
			}

		    return new AutogiroDetailsEventArgs
		    {
		        BankAccountNumber = "123456789",
		        ClearingNumber = "1234",
		        Type = SubscriptionType.SixMonths,
		        ProductPrice = 3500,
		        FeePrice = 255,
		        Title = "Testabonnemang"
		    };
		}

	    public static LensRecipe GetLensRecipe(Article article, ArticleCategory category, ArticleType type, ArticleSupplier supplier)
	    {
	        return new LensRecipe
	        {
				ArticleCategory = category,
				ArticleType = type,
				ArticleSupplier = supplier,
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

		public static LensRecipe GetLensRecipeForLeftEyeOnly(Article article, ArticleCategory category, ArticleType type, ArticleSupplier supplier)
		{
	        return new LensRecipe
	        {
				ArticleCategory = category,
				ArticleType = type,
				ArticleSupplier = supplier,
				Article = new EyeParameter<Article>(article, null),
	            Addition = new EyeParameter<string>("Medium",null),
                Axis = new EyeParameter<string>("25",null),
                Power = new EyeParameter<string>("+3",null),
                BaseCurve = new EyeParameter<decimal?>(4,null),
                Diameter = new EyeParameter<decimal?>(5,null),
                Cylinder = new EyeParameter<string>("-10", null),
				Quantity = new EyeParameter<string>("10 st", null)
	        };
		}

	    public static SubscriptionItem GetSubscriptionItem(Subscription subscription, bool useOngoingSubscription)
	    {
	    	var item = new SubscriptionItem
	    	{
	    	    PerformedWithdrawals = 0, 
                Subscription = subscription,
                Title = "Annas linser"
	    	};
            item.Start();
	    	return useOngoingSubscription ? item.Setup(250, 50, 1250, 125) : item.Setup(3, 1000, 500);
	    }

        public static SubscriptionItem GetSubscriptionItem(Subscription subscription, int index)
        {
            var item = new SubscriptionItem
            {
                PerformedWithdrawals = index,
                Subscription = subscription,
                Title = index.IsOdd() ? "Annas linser" : null
            };
            item.Start();
            return index.IsOdd() ? item.Setup(250, 50, 1250, 125) : item.Setup(index + 2, 1000, 500);
        }

		public static IEnumerable<SubscriptionTransaction> GetTransactions(Subscription subscription)
		{
			var startTime = new DateTime(2011, 01, 01);
			return new[]
			{
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(1), () => GetTransaction(subscription, 1500, TransactionReason.Withdrawal, TransactionType.Withdrawal)),
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(2), () => GetTransaction(subscription, 1025.25m, TransactionReason.Correction, TransactionType.Deposit)),
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(3), () => GetTransaction(subscription, 999.99m, TransactionReason.Correction, TransactionType.Withdrawal)),
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(4), () => GetTransaction(subscription, 275, TransactionReason.PaymentFailed, TransactionType.Deposit)),
				SystemTime.ReturnWhileTimeIs(startTime.AddDays(5), () => GetTransaction(subscription, 275, TransactionReason.Payment, TransactionType.Deposit)),
			};
		}
		private static SubscriptionTransaction GetTransaction(Subscription subscription, decimal amount, TransactionReason reason, TransactionType type)
		{
			var transaction = new SubscriptionTransaction
			{
				Reason = reason, 
				Subscription = subscription, 
				Type = type
			};
			transaction.SetAmount(new SubscriptionAmount(amount,0));
			return transaction;
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