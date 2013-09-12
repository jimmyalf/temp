using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions
{
	public class CountryMap : ClassMap<Country>
	{
		public CountryMap()
		{
			Table("tblSynologenCountry");
			Id(x => x.Id).Column("cId");
			Map(x => x.Name).Column("cName");
		}
	}
}
