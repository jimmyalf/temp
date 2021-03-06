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
			SellingShop = order.SellingShop;
			ContractCompany = order.ContractCompany;
			InvoiceDate = order.InvoiceDate;
		}

		[DataMember] public int CompanyId { get; set; }
		[DataMember] public string CompanyUnit { get; set; }
		[DataMember] public DateTime CreatedDate { get; set; }
		[DataMember] public string CustomerFirstName { get; set; }
		[DataMember] public string Email { get; set; }
		[DataMember] public string CustomerLastName { get; set; }
		[DataMember] public int Id { get; set; }
		[DataMember] public long InvoiceNumber { get; set; }
		[DataMember] public bool MarkedAsPayedByShop { get; set; }
		[DataMember] public string PersonalIdNumber { get; set; }
		[DataMember] public string Phone { get; set; }
		[DataMember] public string RstText { get; set; }
		[DataMember] public int SalesPersonShopId { get; set; }
		[DataMember] public int StatusId { get; set; }
		[DataMember] public DateTime UpdateDate { get; set; }
		[DataMember] public int SalesPersonMemberId { get; set; }
		[DataMember] public double InvoiceSumIncludingVAT { get; set; }
		[DataMember] public double InvoiceSumExcludingVAT { get; set; }
		[DataMember] public string CustomerOrderNumber { get; set; }
		public string CustomerCombinedName {
			get { return String.Concat( CustomerFirstName ?? String.Empty,  " ",  CustomerLastName ?? String.Empty ).Trim(); }
		}
		public string PersonalBirthDateString {
			get {
				if(PersonalIdNumber == null) return null;
				return (PersonalIdNumber.Length < 12) ? null : PersonalIdNumber.Substring(0, 8);
			}
		}
		[DataMember] public IList<OrderItem> OrderItems{ get; set; }
		[DataMember] public Shop SellingShop{ get; set; }
		[DataMember] public Company ContractCompany{ get; set; }

		public DateTime? InvoiceDate { get; protected set; }

		public virtual string ParseFreeText()
		{
			return ParseFreeText(ContractCompany, this, SellingShop);
		}

		public static string ParseFreeText(ICompany company, IOrder order, IShop shop)
		{
			if(company == null || company.InvoiceFreeTextFormat == null) return null;
			var output = company.InvoiceFreeTextFormat;
			if(order != null)
			{
				output = output
					.Replace("{CustomerName}", order.CustomerCombinedName ?? String.Empty)
					.Replace("{CustomerPersonalIdNumber}", order.PersonalIdNumber ?? String.Empty)
					.Replace("{CompanyUnit}", order.CompanyUnit ?? String.Empty)
					.Replace("{CustomerPersonalBirthDateString}", order.PersonalBirthDateString ?? String.Empty)
					.Replace("{CustomerFirstName}", order.CustomerFirstName ?? String.Empty)
					.Replace("{CustomerLastName}", order.CustomerLastName ?? String.Empty)
					.Replace("{BuyerCompanyId}", order.CompanyId.ToString())
					.Replace("{RST}", order.RstText ?? String.Empty);
			}
			if(!Equals(company, default(ICompany)))
			{
				output = output.Replace("{BankCode}", company.BankCode ?? String.Empty);
			}
			if(shop != null)
			{
				output = output
					.Replace("{SellingShopName}", shop.Name ?? String.Empty)
					.Replace("{SellingShopNumber}", shop.Number ?? String.Empty)
					.Replace("{SellingShopOrgNumber}", shop.OrganizationNumber ?? String.Empty);
			}
			return output;
		}
	}
}