using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class EyeParameterComponentMap : ComponentMap<EyeParameter>
    {
        public EyeParameterComponentMap()
        {
            Map(x => x.Left);
            Map(x => x.Right);
        }
    }
}