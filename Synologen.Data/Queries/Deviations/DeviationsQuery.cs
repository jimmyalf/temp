using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.SqlCommand;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Queries.Deviations
{
    public class DeviationsQuery : Query<IExtendedEnumerable<Deviation>>
    {
        public DeviationType? SelectedType { get; set; }
        public int? SelectedCategory { get; set; }
        public int? SelectedSupplier { get; set; }
        public string SearchTerms { get; set; }
        public PagedSortedCriteria PagedSortedCriteria { get; set; }

        public override IExtendedEnumerable<Deviation> Execute()
        {
            var result = Session
                .CreateCriteriaOf<Deviation>()
                .CreateAlias(x => x.Category)
                .CreateAlias(x => x.Supplier);

            if (SelectedType.HasValue)
                result = result.FilterEqual(x => x.Type, SelectedType);

            if (SelectedSupplier > 0)
                result = result.FilterEqual(x => x.Supplier.Id, SelectedSupplier);

            if (SelectedCategory > 0)
                result = result.FilterEqual(x => x.Category.Id, SelectedCategory);

            result = result.SynologenFilterByAny(filter =>
                {
                    filter.Like(x => x.Category.Name);
                    filter.Like(x => x.Supplier.Name);
                }, SearchTerms);

            if (PagedSortedCriteria != null)
            {
                var count = CriteriaTransformer.Clone(result).SetProjection(Projections.RowCountInt64()).UniqueResult<long>();
                var list = result
                    .Sort(PagedSortedCriteria.OrderBy, PagedSortedCriteria.SortAscending)
                    .Page(PagedSortedCriteria.Page, PagedSortedCriteria.PageSize)
                    .List<Deviation>();
                return new ExtendedEnumerable<Deviation>(list, count, PagedSortedCriteria.Page, PagedSortedCriteria.PageSize, PagedSortedCriteria.OrderBy, PagedSortedCriteria.SortAscending);
            }
            return new ExtendedEnumerable<Deviation>(result.List<Deviation>());
        }

    }
}