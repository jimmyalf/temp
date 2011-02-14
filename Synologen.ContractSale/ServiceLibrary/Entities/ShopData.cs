using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.ServiceLibrary.Entities{
	[DataContract]
	public class ShopData : IShop {

		public ShopData(IShop shop) {
			ShopId = shop.ShopId;
			Name = shop.Name;
			Number = shop.Number;
			Description = shop.Description;
			Active = shop.Active;
			Address = shop.Address;
			Address2 = shop.Address2;
			Zip = shop.Zip;
			City = shop.City;
			Phone = shop.Phone;
			Phone2 = shop.Phone2;
			Fax = shop.Fax;
			Email = shop.Email;
			ContactFirstName = shop.ContactFirstName;
			ContactLastName = shop.ContactLastName;
			CategoryId = shop.CategoryId;
		}

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
		public IList<IShop> GetAllShopsInConcern(ISqlProvider sqlProvider){
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