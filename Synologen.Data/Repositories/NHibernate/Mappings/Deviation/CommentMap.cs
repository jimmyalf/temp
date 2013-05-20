using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Deviations
{
	public class DeviationCommentMap : ClassMap<DeviationComment>
	{
        public DeviationCommentMap()
        {
            Table("SynologenDeviationComments");
            Id(x => x.Id);
            Map(x => x.Description);
            Map(x => x.CreatedDate);
        }

	}
}
