using System.Collections.Generic;
using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Queries.Deviations
{
    public class CategoriesQuery : Query<IExtendedEnumerable<DeviationCategory>>
    {
        public int? SelectedCategory { get; set; }
        public bool? Active { get; set; }
        public string SearchTerms { get; set; }
        public PagedSortedCriteria PagedSortedCriteria { get; set; }

        public override IExtendedEnumerable<DeviationCategory> Execute()
        {
            var result = Session
                .CreateCriteriaOf<DeviationCategory>();

            result = result.SynologenFilterByAny(filter =>
            {
                filter.Like(x => x.Name);
            }, SearchTerms);

            if (Active.HasValue)
            {
                result = result.FilterEqual(x => x.Active, Active);
            }

            if (PagedSortedCriteria != null)
            {
                var count = CriteriaTransformer.Clone(result).SetProjection(Projections.RowCountInt64()).UniqueResult<long>();

                var list = result
                .Sort(PagedSortedCriteria.OrderBy, PagedSortedCriteria.SortAscending)
                .Page(PagedSortedCriteria.Page, PagedSortedCriteria.PageSize)
                .List<DeviationCategory>();
                return new ExtendedEnumerable<DeviationCategory>(list, count, PagedSortedCriteria.Page, PagedSortedCriteria.PageSize, PagedSortedCriteria.OrderBy, PagedSortedCriteria.SortAscending);
            }

            return new ExtendedEnumerable<DeviationCategory>(result.List<DeviationCategory>());
        }

    }
}