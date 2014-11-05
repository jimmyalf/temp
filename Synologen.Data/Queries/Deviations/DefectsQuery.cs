using System.Collections.Generic;
using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using NHibernate.Criterion;

namespace Spinit.Wpc.Synologen.Data.Queries.Deviations
{
    public class DefectsQuery : Query<IList<DeviationDefect>>
    {
        public int? SelectedCategory { get; set; }
        public int? SelectedDefect { get; set; }

        public override IList<DeviationDefect> Execute()
        {
            ICriteria result = Session.CreateCriteriaOf<DeviationDefect>();

            if (SelectedCategory.HasValue)
            {
                result = ((ICriteria<DeviationDefect>)result).FilterEqual(x => x.Category.Id, SelectedCategory);
            }

            return result.AddOrder(Order.Asc("Name")).List<DeviationDefect>();
        }

    }
}