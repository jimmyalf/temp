using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Data.Queries.Deviations
{
	public class DeviationsQuery : Query<IList<Deviation>>
	{
		public DeviationType? SelectedDeviationType { get; set; }

		public override IList<Deviation> Execute()
		{
			if (SelectedDeviationType.HasValue)
			{
				// TODO: Filter results on selected deviation type
			}

			// TODO: Implement query
			return new List<Deviation>();
		}
	}
}