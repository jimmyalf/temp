using System.Collections.Generic;
using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using NHibernate.Criterion;

namespace Spinit.Wpc.Synologen.Data.Queries.Deviations
{
    public class SuppliersQuery : Query<IList<DeviationSupplier>>
    {
        public int? SelectedCategory { get; set; }
        public bool? Active { get; set; }
        public string SearchTerms { get; set; }

        public override IList<DeviationSupplier> Execute()
        {
            ICriteria result = Session.CreateCriteriaOf<DeviationSupplier>();

            if (Active.HasValue)
            {
                result = ((ICriteria<DeviationSupplier>)result).FilterEqual(x => x.Active, Active);
            }

            if (SelectedCategory.HasValue)
            {
                result = result
                    .CreateCriteria("Categories")
                    .Add(Restrictions.Eq("Id", SelectedCategory.Value));
            }

            if (!string.IsNullOrEmpty(SearchTerms))
            {
                ((ICriteria<DeviationSupplier>)result).FilterEqual(x => x.Name, SearchTerms);
            }

            return result.AddOrder(Order.Asc("Name")).List<DeviationSupplier>();

        }

    }
}