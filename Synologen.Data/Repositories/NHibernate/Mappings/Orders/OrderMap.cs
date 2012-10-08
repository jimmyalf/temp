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
			Component(x => x.WithdrawalAmount,mapping =>
			{
			    mapping.Map(x => x.Taxed).Column("TaxedWithdrawalAmount");
			    mapping.Map(x => x.TaxFree).Column("TaxFreeWithdrawalAmount");
			});
			Map(x => x.Status).CustomType<OrderStatus>();
        	Map(x => x.Reference).Nullable().Length(255);
            References(x => x.LensRecipe).Column("LensRecipeId");
        	References(x => x.SubscriptionPayment).Column("SubscriptionItemId").Nullable();
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