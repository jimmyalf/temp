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
            Map(x => x.SpinitServicesEmailId);
            Map(x => x.ShippingType).CustomType<OrderShippingOption>();
        	Map(x => x.OrderTotalWithdrawalAmount).Nullable();
			Map(x => x.Status).CustomType<OrderStatus>();
            References(x => x.LensRecipe).Column("LensRecipeId");
        	References(x => x.SubscriptionPayment).Column("SubscriptionItemId").Nullable();
            References(x => x.Article).Column("ArticleId");
        	References(x => x.Customer).Column("CustomerId").Not.Nullable();
        	References(x => x.Shop).Column("ShopId").Not.Nullable();
        	Component(x => x.SelectedPaymentOption, paymentOption =>
        	{
        		paymentOption.Map(x => x.Type).Column("PaymentOptionType").CustomType<PaymentOptionType>();
				paymentOption.Map(x => x.SubscriptionId).Column("PaymentOptionSubscripitonId").Nullable();
        	});
        }
    }
}