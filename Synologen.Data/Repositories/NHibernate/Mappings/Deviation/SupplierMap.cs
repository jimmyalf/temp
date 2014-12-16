using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Deviations
{
	public class DeviationSupplierMap : ClassMap<DeviationSupplier>
	{
        public DeviationSupplierMap()
        {
            Table("SynologenDeviationSuppliers");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.Phone);
            Map(x => x.Active);

            HasManyToMany(x => x.Categories).Table("SynologenDeviationSupplierToDeviationCategory").ParentKeyColumn("SupplierId").ChildKeyColumn("CategoryId").Cascade.All();
        }

	}
}