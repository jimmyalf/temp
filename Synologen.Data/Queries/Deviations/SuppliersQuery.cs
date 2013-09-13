using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.SqlCommand;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Queries.Deviations
{
    public class SuppliersQuery : Query<IExtendedEnumerable<DeviationSupplier>>
    {
        public int? SelectedCategory { get; set; }
        public bool? Active { get; set; }
        public string SearchTerms { get; set; }
        public PagedSortedCriteria PagedSortedCriteria { get; set; }

        public override IExtendedEnumerable<DeviationSupplier> Execute()
        {
            var result = Session
                .CreateCriteriaOf<DeviationSupplier>();

            result = result.SynologenFilterByAny(filter =>
            {
                filter.Like(x => x.Name);
            }, SearchTerms);

            if (SelectedCategory.HasValue)
            {
                result = (ICriteria<DeviationSupplier>)result.CreateAlias("Categories", "Category").In("Category.Id", SelectedCategory);
            }


            if (PagedSortedCriteria != null)
            {
                var count =
                    CriteriaTransformer.Clone(result).SetProjection(Projections.RowCountInt64()).UniqueResult<long>();

                var list = result
                    .Sort(PagedSortedCriteria.OrderBy, PagedSortedCriteria.SortAscending)
                    .Page(PagedSortedCriteria.Page, PagedSortedCriteria.PageSize)
                    .List<DeviationSupplier>();

                return new ExtendedEnumerable<DeviationSupplier>(list, count, PagedSortedCriteria.Page,
                                                                 PagedSortedCriteria.PageSize,
                                                                 PagedSortedCriteria.OrderBy,
                                                                 PagedSortedCriteria.SortAscending);
            }
            return new ExtendedEnumerable<DeviationSupplier>(result.List<DeviationSupplier>());
        }

    }
}