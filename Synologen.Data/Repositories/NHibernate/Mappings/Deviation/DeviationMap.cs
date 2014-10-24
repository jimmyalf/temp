using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Deviations
{
	public class DeviationMap : ClassMap<Deviation>
	{
        public DeviationMap()
        {
            Table("SynologenDeviations");
            Id(x => x.Id);
            Map(x => x.ShopId);
            Map(x => x.Status).Column("StatusId").CustomType<DeviationStatus>();
            Map(x => x.CreatedDate);
            Map(x => x.Type).Column("TypeId").CustomType<DeviationType>();
            Map(x => x.DefectDescription);
            Map(x => x.Title);

            References(x => x.Category).Column("CategoryId");
            References(x => x.Supplier).Column("SupplierId");
            HasManyToMany(x => x.Defects).Table("SynologenDeviationDefectToDeviation").ParentKeyColumn("DeviationId").ChildKeyColumn("DefectId").Cascade.All();
            HasManyToMany(x => x.Comments).Table("SynologenDeviationCommentToDeviation").ParentKeyColumn("DeviationId").ChildKeyColumn("CommentId").Cascade.All();
        }

	}
}