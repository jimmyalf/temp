using System.Collections.Generic;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.Orders
{
    public static class OrderFactory
    {
        public static IEnumerable<ArticleCategory> GetCategories()
        {
            return Sequence.Generate(GetCategory, 15);
        }

        public static ArticleCategory GetCategory()
        {
            return new ArticleCategory { Name = "Artikel 1" };
        }

        public static OrderCustomer GetCustomer()
        {
            return new OrderCustomer { AddressLineOne = "Box 1234", AddressLineTwo = "Datavägen 23", City = "Mölndal", Email = "adam.b@testbolaget.se", FirstName = "Bertil", LastName = "Adamsson", MobilePhone = "0701-987654", Notes = "Anteckningar ABC DEF", PersonalIdNumber = "197001013239", Phone = "031123456", PostalCode = "41300", };
        }

        public static IEnumerable<ArticleSupplier> GetSuppliers()
        {
            return Sequence.Generate(() => GetSupplier(), 20);
        }

        public static ArticleSupplier GetSupplier(int id = 6, OrderShippingOption shippingOptions = OrderShippingOption.DeliveredInStore | OrderShippingOption.ToCustomer | OrderShippingOption.ToStore)
        {
            var fakeSupplier = A.Fake<ArticleSupplier>();
            A.CallTo(() => fakeSupplier.Id).Returns(id);
            fakeSupplier.Name = "Leverantör ABC";
            fakeSupplier.ShippingOptions = shippingOptions;
            return fakeSupplier;
        }

        public static IEnumerable<ArticleType> GetArticleTypes()
        {
            return Sequence.Generate(GetArticleType, 15);
        }

        public static ArticleType GetArticleType()
        {
            return new ArticleType { Name = "Endagslinser" };
        }

        public static IEnumerable<Article> GetArticles()
        {
            return Sequence.Generate(GetArticle, 15);
        }
        public static Article GetArticle(int id = 2)
        {
            var fakeOrderArticle = A.Fake<Article>();
            A.CallTo(() => fakeOrderArticle.Id).Returns(id);
            fakeOrderArticle.Name = "Lins 1337";
            fakeOrderArticle.Options = new ArticleOptions
			{
				EnableAxis = false,
                BaseCurve = new SequenceDefinition(-1, 2, 0.25M),
                Diameter = new SequenceDefinition(-1, 2, 0.25M),
            };
            
            return fakeOrderArticle;
        }

    	public static Order GetOrder(Article article = null, OrderCustomer customer = null, LensRecipe lensRecipe = null, int? selectedSubscriptionId = null, SubscriptionItem subscriptionItem = null, OrderShippingOption? shippingType = null)
    	{
			return new Order
			{
				Article = new EyeParameter<Article>
				{
					Left = article ?? GetArticle(),
					Right = article ?? GetArticle(),
				},
				Customer = customer ?? GetCustomer(),
				LensRecipe = lensRecipe,
				SelectedPaymentOption = selectedSubscriptionId.HasValue
					? new PaymentOption
					{
						SubscriptionId = selectedSubscriptionId,
						Type = PaymentOptionType.Subscription_Autogiro_Existing
					}
					: null,
				ShippingType = shippingType ?? OrderShippingOption.DeliveredInStore | OrderShippingOption.ToCustomer | OrderShippingOption.ToStore,
				SubscriptionPayment = subscriptionItem
			};
    	}
    }
}