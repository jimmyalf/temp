using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings
{
    public class FrameSupplierMap : ClassMap<FrameSupplier>
    {
        public FrameSupplierMap()
        {
            Table("SynologenFrameSupplier");
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Email).Not.Nullable();
        }
    }
}