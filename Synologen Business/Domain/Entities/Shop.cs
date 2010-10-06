using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract]
	public class Shop : IShop
	{
		[DataMember] public int ShopId { get; set; }
		[DataMember] public string Name { get; set; }
		[DataMember] public string Number { get; set; }
		[DataMember] public string Description { get; set; }
		[DataMember] public bool Active { get; set; }
		[DataMember] public string Address { get; set; }
		[DataMember] public string Address2 { get; set; }
		[DataMember] public string Zip { get; set; }
		[DataMember] public string City { get; set; }
		[DataMember] public string Phone { get; set; }
		[DataMember] public string Phone2 { get; set; }
		[DataMember] public string Fax { get; set; }
		[DataMember] public string Email { get; set; }
		[DataMember] public string ContactFirstName { get; set; }
		[DataMember] public string ContactLastName { get; set; }
		[DataMember] public int CategoryId { get; set; }
		public IList<IShop> GetAllShopsInConcern(ISqlProvider sqlProvider) {
			if(Concern == null || Concern.Id <= 0) return new List<IShop>();
			return sqlProvider.GetShopRows(null, null, null, null, null, null, Concern.Id, null).ToList();
		}
		[DataMember] public IEnumerable<ShopEquipment> Equipment { get; set; }
		[DataMember] public string Url { get; set; }
		[DataMember] public string MapUrl { get; set; }
		[DataMember] public int GiroId { get; set; }
		[DataMember] public string GiroNumber { get; set; }
		[DataMember] public string GiroSupplier { get; set; }
		[DataMember] public Concern Concern { get; set; }
		public string ContactCombinedName {
			get { return String.Concat( ContactFirstName ?? String.Empty,  " ",  ContactLastName ?? String.Empty ).Trim(); }
		}
		public bool HasConcern { get{ return Concern != null;} }
		public ShopAccess Access { get; set; }
	}
}