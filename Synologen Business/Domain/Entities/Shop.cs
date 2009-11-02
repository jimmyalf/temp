using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract]
	public class Shop : IShop{
		public int ShopId { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string Description { get; set; }
		public bool Active { get; set; }
		public string Address { get; set; }
		public string Address2 { get; set; }
		public string Zip { get; set; }
		public string City { get; set; }
		public string Phone { get; set; }
		public string Phone2 { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
		public string ContactFirstName { get; set; }
		public string ContactLastName { get; set; }
		public int CategoryId { get; set; }
		public IList<IShop> GetAllShopsInConcern(ISqlProvider sqlProvider) {
			if(Concern == null || Concern.Id <= 0) return new List<IShop>();
			return sqlProvider.GetShopRows(null, null, null, null, null, null, Concern.Id, null).ToList();
		}
		public IEnumerable<ShopEquipment> Equipment { get; set; }
		public string Url { get; set; }
		public string MapUrl { get; set; }
		public int GiroId { get; set; }
		public string GiroNumber { get; set; }
		public string GiroSupplier { get; set; }
		public IConcern Concern { get; set; }
		public string ContactCombinedName {
			get { return String.Concat( ContactFirstName ?? String.Empty,  " ",  ContactLastName ?? String.Empty ).Trim(); }
		}
		public bool HasConcern { get{ return Concern != null;} }
	}
}