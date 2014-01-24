using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Deviations
{
	public class DeviationCategoryMap : ClassMap<DeviationCategory>
	{
        public DeviationCategoryMap()
        {
            Table("SynologenDeviationCategories");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Active);

            HasMany(x => x.Deviations).KeyColumn("CategoryId").Inverse();

            HasMany(x => x.Defects).KeyColumn("CategoryId").Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.Suppliers).Table("SynologenDeviationSupplierToDeviationCategory").ParentKeyColumn("CategoryId").ChildKeyColumn("SupplierId").Inverse();
        }

	}
}
