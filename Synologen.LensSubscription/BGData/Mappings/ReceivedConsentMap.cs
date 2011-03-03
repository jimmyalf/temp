using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
    public class ReceivedConsentMap : ClassMap<BGReceivedConsent>
    {
        public ReceivedConsentMap()
        {
            Table("ReceivedConsents");
			Id(x => x.Id);
			//Map(x => x.PayerNumber).Not.Nullable();
			References(x => x.Payer).Column("PayerId").Not.Nullable();
            Map(x => x.ActionDate).Not.Nullable();
			Map(x => x.ConsentValidForDate).Nullable();
			Map(x => x.InformationCode)
                .CustomType(typeof(ConsentInformationCode))
                .Nullable();
            Map(x => x.CommentCode)
                .CustomType(typeof(ConsentCommentCode))
                .Not.Nullable();
            Map(x => x.CreatedDate).Not.Nullable();
        }
    }
}
