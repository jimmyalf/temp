using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("SynologenOrder");
            Id(x => x.Id);

            Map(x => x.Created);

            Map(x => x.ShippingType).CustomType<int>();
            References(x => x.LensRecipe).Column("LensRecipeId");
        	References(x => x.SubscriptionPayment).Column("SubscriptionItemId").Nullable();
            References(x => x.Article).Column("ArticleId");
        	References(x => x.Customer).Column("CustomerId").Not.Nullable();
        	Component(x => x.SelectedPaymentOption, paymentOption =>
        	{
        		paymentOption.Map(x => x.Type).Column("PaymentOptionType").CustomType<int>();
				paymentOption.Map(x => x.SubscriptionId).Column("PaymentOptionSubscripitonId").Nullable();
        	});
        }
    }
}