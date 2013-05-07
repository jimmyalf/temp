using System.Collections.Generic;
using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using NHibernate.Criterion;

namespace Spinit.Wpc.Synologen.Data.Queries.Deviations
{
    public class CategoriesQuery : Query<IList<DeviationCategory>>
	{
        public int? SelectedCategory { get; set; }
        public bool? Active { get; set; }
        public string SearchTerms { get; set; }

        public override IList<DeviationCategory> Execute()
        {
            ICriteria result = Session.CreateCriteriaOf<DeviationCategory>();

            if (SelectedCategory.HasValue)
			{
                result = ((ICriteria<DeviationCategory>)result).FilterEqual(x => x.Id, SelectedCategory);
			}

            if (Active.HasValue)
            {
                result = ((ICriteria<DeviationCategory>)result).FilterEqual(x => x.Active, Active);
            }

            if (!string.IsNullOrEmpty(SearchTerms))
            {
                result = ((ICriteria<DeviationCategory>)result).FilterEqual(x => x.Name, SearchTerms);
            }

            return result.AddOrder(Order.Asc("Name")).List<DeviationCategory>();
		}
        
	}
}