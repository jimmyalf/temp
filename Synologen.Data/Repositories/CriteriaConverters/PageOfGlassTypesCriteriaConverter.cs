using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
    public class PageOfGlassTypesCriteriaConverter : NHibernateActionCriteriaConverter<PageOfGlassTypesMatchingCriteria, FrameGlassType>
    {
        public PageOfGlassTypesCriteriaConverter(ISession session) : base(session) { }

        public override ICriteria Convert(PageOfGlassTypesMatchingCriteria source)
        {
            return Criteria
                .CreateAlias(x => x.Supplier)
                .Sort(source.OrderBy, source.SortAscending)
                .Page(source.Page, source.PageSize);
        }
    }
}