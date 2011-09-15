using System.Collections.Generic;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces
{
	public interface IShop
	{
		[DataMember] int ShopId { get; set; }
		[DataMember] string Name { get; set; }
		[DataMember] string Number { get; set; }
		[DataMember] string Description { get; set; }
		[DataMember] bool Active { get; set; }
		[DataMember] string Address { get; set; }
		[DataMember] string Address2 { get; set; }
		[DataMember] string Zip { get; set; }
		[DataMember] string City { get; set; }
		[DataMember] string Phone { get; set; }
		[DataMember] string Phone2 { get; set; }
		[DataMember] string Fax { get; set; }
		[DataMember] string Email { get; set; }
		[DataMember] string ContactFirstName { get; set; }
		[DataMember] string ContactLastName { get; set; }
		[DataMember] int CategoryId { get; set; }
		IList<IShop> GetAllShopsInConcern(ISqlProvider sqlProvider);
		[DataMember] IEnumerable<ShopEquipment> Equipment { get; set; }
		[DataMember] string Url { get; set; }
		[DataMember] string MapUrl { get; set; }
		[DataMember] int GiroId { get; set; }
		[DataMember] string GiroNumber { get; set; }
		[DataMember] string GiroSupplier { get; set; }
		[DataMember] Concern Concern { get; set; }
		string ContactCombinedName { get; }
		bool HasConcern { get; }
		ShopAccess Access { get; set; }
		string OrganizationNumber { get; set; }
	}
}