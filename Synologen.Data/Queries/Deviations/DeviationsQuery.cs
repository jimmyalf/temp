﻿using System.Collections;
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
        public int? SelectedDeviation { get; set; }
        public string SearchTerms { get; set; }
        public PagedSortedCriteria PagedSortedCriteria { get; set; }

        public override IExtendedEnumerable<Deviation> Execute()
        {
            var result = Session
                .CreateCriteriaOf<Deviation>().CreateAlias(x => x.Category);

            if (SelectedDeviation.HasValue)
                return new ExtendedEnumerable<Deviation>(result.FilterEqual(x => x.Id, SelectedDeviation).List<Deviation>());

            if (SelectedType.HasValue)
            {
                if (SelectedType == DeviationType.External)
                {
                    result = result.CreateAlias(x => x.Supplier);
                }

                result = result.FilterEqual(x => x.Type, SelectedType);
            }
            else
            {
                //result = result.CreateAlias(x => x.Supplier);
                result = (ICriteria<Deviation>) result.CreateAlias("Supplier", "Supplier", JoinType.LeftOuterJoin);
            }

            if (SelectedSupplier > 0)
                result = result.FilterEqual(x => x.Supplier.Id, SelectedSupplier);

            if (SelectedCategory > 0)
                result = result.FilterEqual(x => x.Category.Id, SelectedCategory);

            if (!string.IsNullOrEmpty(SearchTerms))
            {
                result = result.SynologenFilterByAny(filter =>
                {
                    filter.Like(x => x.Category.Name);
                    filter.Like(x => x.Supplier.Name);
                }, SearchTerms);
            }

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