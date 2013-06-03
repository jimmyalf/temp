using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Deviations
{
	public class DeviationDefectMap : ClassMap<DeviationDefect>
	{
        public DeviationDefectMap()
        {
            Table("SynologenDeviationDefects");
            Id(x => x.Id);
            Map(x => x.Name);
            
            References(x => x.Category).Column("CategoryId");
        }

	}
}
