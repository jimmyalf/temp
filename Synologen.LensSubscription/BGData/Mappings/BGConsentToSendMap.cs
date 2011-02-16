using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
	public class BGConsentToSendMap : ClassMap<BGConsentToSend>
	{
		public BGConsentToSendMap()
		{
			Id(x => x.Id);
			Component(x => x.Account, map =>
			{
				map.Map(x => x.AccountNumber).Not.Nullable();
				map.Map(x => x.ClearingNumber).Not.Nullable();
			});
			Map(x => x.OrgNumber).Nullable();
			Map(x => x.PayerNumber).Not.Nullable();
			Map(x => x.PersonalIdNumber).Nullable();
			Map(x => x.SendDate).Nullable();
			Map(x => x.Type).CustomType(typeof (ConsentType)).Not.Nullable();
		}
	}
}