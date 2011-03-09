using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
    public class ReceivedPaymentMap : ClassMap<BGReceivedPayment>
    {
        public ReceivedPaymentMap()
        {
            Table("ReceivedPayments");
            Id(x => x.Id);
            //Map(x => x.PayerNumber).Not.Nullable();
			References(x => x.Payer).Column("PayerId").Not.Nullable();
            Map(x => x.Amount).Not.Nullable();
            Map(x => x.ResultType)
                .CustomType(typeof (PaymentResult))
                .Not.Nullable();
            Map(x => x.Reference).Nullable();
            Map(x => x.PaymentDate).Not.Nullable();
            Map(x => x.CreatedDate).Not.Nullable();
        }
    }
}
