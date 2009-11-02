using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract]
	public class Order : IOrder {

		public Order() {  }

		public Order(IOrder order) {
			CompanyId = order.CompanyId;
			CompanyUnit = order.CompanyUnit;
			CreatedDate = order.CreatedDate;
			CustomerFirstName = order.CustomerFirstName;
			CustomerLastName = order.CustomerLastName;
			Email = order.Email;
			Id = order.Id;
			InvoiceNumber = order.InvoiceNumber;
			MarkedAsPayedByShop = order.MarkedAsPayedByShop;
			PersonalIdNumber = order.PersonalIdNumber;
			Phone = order.Phone;
			RstText = order.RstText;
			SalesPersonShopId = order.SalesPersonShopId;
			StatusId = order.StatusId;
			UpdateDate = order.UpdateDate;
			SalesPersonMemberId = order.SalesPersonMemberId;
			InvoiceSumIncludingVAT = order.InvoiceSumIncludingVAT;
			InvoiceSumExcludingVAT = order.InvoiceSumExcludingVAT;
			CustomerOrderNumber = order.CustomerOrderNumber;
			OrderItems = order.OrderItems;
			SellingShop = SellingShop;
			ContractCompany = ContractCompany;
		}

		public int CompanyId { get; set; }
		public string CompanyUnit { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CustomerFirstName { get; set; }
		public string Email { get; set; }
		public string CustomerLastName { get; set; }
		public int Id { get; set; }
		public long InvoiceNumber { get; set; }
		public bool MarkedAsPayedByShop { get; set; }
		public string PersonalIdNumber { get; set; }
		public string Phone { get; set; }
		public string RstText { get; set; }
		public int SalesPersonShopId { get; set; }
		public int StatusId { get; set; }
		public DateTime UpdateDate { get; set; }
		public int SalesPersonMemberId { get; set; }
		public double InvoiceSumIncludingVAT { get; set; }
		public double InvoiceSumExcludingVAT { get; set; }
		public string CustomerOrderNumber { get; set; }
		public string CustomerCombinedName {
			get { return String.Concat( CustomerFirstName ?? String.Empty,  " ",  CustomerLastName ?? String.Empty ).Trim(); }
		}
		public string PersonalBirthDateString {
			get {
				if(PersonalIdNumber == null) return null;
				return (PersonalIdNumber.Length < 12) ? null : PersonalIdNumber.Substring(0, 8);
			}
		}
		public IList<IOrderItem> OrderItems{ get; set; }
		public IShop SellingShop{ get; set; }
		public ICompany ContractCompany{ get; set; }
	}
}