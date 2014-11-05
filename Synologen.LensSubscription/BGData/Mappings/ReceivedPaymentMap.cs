using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
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
			References(x => x.Payer).Column("PayerId").Not.Nullable();
            Map(x => x.Amount).Not.Nullable();
            Map(x => x.ResultType)
                .CustomType(typeof (PaymentResult))
                .Not.Nullable();
            Map(x => x.Reference).Nullable();
            Map(x => x.PaymentDate).Not.Nullable();
            Map(x => x.CreatedDate).Not.Nullable();
        	Map(x => x.Handled).Not.Nullable();
            Map(x => x.Type)
                .CustomType(typeof (PaymentType))
                .Not.Nullable();
            Map(x => x.PeriodCode)
                .CustomType(typeof (PaymentPeriodCode))
                .Not.Nullable();
            Map(x => x.NumberOfReoccuringTransactionsLeft).Nullable();
            Component(x => x.Reciever, map =>
            {
                map.Map(x => x.BankgiroNumber).Not.Nullable();
                map.Map(x => x.CustomerNumber).Not.Nullable();
            });
        }
    }
}
