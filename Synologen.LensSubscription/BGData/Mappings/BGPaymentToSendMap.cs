using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
	public class BGPaymentToSendMap : ClassMap<BGPaymentToSend>
	{
		public BGPaymentToSendMap()
		{
			Id(x => x.Id);
			Map(x => x.Amount).Not.Nullable();
			References(x => x.Payer).Column("PayerId").Not.Nullable();
			Map(x => x.PaymentDate).Not.Nullable();
			Map(x => x.PaymentPeriodCode).CustomType(typeof(PaymentPeriodCode)).Not.Nullable();
			Map(x => x.Reference).Nullable();
			Map(x => x.SendDate).Nullable();
			Map(x => x.Type).CustomType(typeof (PaymentType)).Not.Nullable();
		}
	}
}