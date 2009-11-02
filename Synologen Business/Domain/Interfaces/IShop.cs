using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IShop{
		int ShopId { get; set; }
		string Name { get; set; }
		string Number { get; set; }
		string Description { get; set; }
		bool Active { get; set; }
		string Address { get; set; }
		string Address2 { get; set; }
		string Zip { get; set; }
		string City { get; set; }
		string Phone { get; set; }
		string Phone2 { get; set; }
		string Fax { get; set; }
		string Email { get; set; }
		string ContactFirstName { get; set; }
		string ContactLastName { get; set; }
		int CategoryId { get; set; }
		IList<IShop> GetAllShopsInConcern(ISqlProvider sqlProvider);
		IEnumerable<ShopEquipment> Equipment { get; set; }
		string Url { get; set; }
		string MapUrl { get; set; }
		int GiroId { get; set; }
		string GiroNumber { get; set; }
		string GiroSupplier { get; set; }
		IConcern Concern { get; set; }
		string ContactCombinedName { get; }
		bool HasConcern { get; }
	}
}