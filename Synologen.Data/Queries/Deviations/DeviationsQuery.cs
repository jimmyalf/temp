using System.Collections.Generic;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Queries.Deviations
{
	public class DeviationsQuery : Query<IList<Deviation>>
	{
		public DeviationType? SelectedType { get; set; }
        public int? SelectedCategory { get; set; }
        public int? SelectedSupplier { get; set; }

		public override IList<Deviation> Execute()
		{
            ICriteria result = Session.CreateCriteriaOf<Deviation>();

			if (SelectedType.HasValue)
			{
                result = ((ICriteria<Deviation>)result).FilterEqual(x => x.Type, SelectedType);
			}

            if (SelectedSupplier > 0)
            {
                result = ((ICriteria<Deviation>)result).FilterEqual(x => x.Supplier.Id, SelectedSupplier);
            }

            if (SelectedCategory > 0)
            {
                result = ((ICriteria<Deviation>)result).FilterEqual(x => x.Category.Id, SelectedCategory);
            }

            return result.AddOrder(Order.Desc("CreatedDate")).List<Deviation>();
		}
        
	}
}