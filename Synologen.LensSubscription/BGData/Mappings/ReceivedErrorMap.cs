using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
    public class ReceivedErrorMap : ClassMap<BGReceivedError>
    {
        public ReceivedErrorMap()
        {
            Table("ReceivedError");
            Id(x => x.Id);
            Map(x => x.Amount).Not.Nullable();
            Map(x => x.PayerNumber).Not.Nullable();
            Map(x => x.CommentCode)
                .CustomType(typeof(ErrorCommentCode))
                .Not.Nullable();
            Map(x => x.PaymentDate)
                .Not.Nullable();
            Map(x => x.Reference).Nullable();
            Map(x => x.CreatedDate).Not.Nullable();
        }
    }
}
