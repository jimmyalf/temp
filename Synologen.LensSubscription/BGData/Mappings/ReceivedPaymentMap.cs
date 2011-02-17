using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
    public class ReceivedPaymentMap : ClassMap<BGReceivedPayment>
    {
        public ReceivedPaymentMap()
        {
            Table("ReceivedPayments");
            Id(x => x.Id);
            Map(x => x.PayerNumber);
            Map(x => x.Amount);
            Map(x => x.ResultType)
                .CustomType(typeof (PaymentResult));
            Map(x => x.Reference)
                .Nullable();
            Map(x => x.PaymentDate);
            Map(x => x.CreatedDate);
        }
    }
}
