using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.ServiceLibrary{
	[DataContract]
	public class OrderItemData : IOrderItem{

		public OrderItemData(IOrderItem orderItem) {
			Id = orderItem.Id;
			ArticleId = orderItem.ArticleId;
			ArticleDisplayName = orderItem.ArticleDisplayName;
			SinglePrice = orderItem.SinglePrice;
			NumberOfItems = orderItem.NumberOfItems;
			Notes = orderItem.Notes;
			ArticleDisplayNumber = orderItem.ArticleDisplayNumber;
			DisplayTotalPrice = orderItem.DisplayTotalPrice;
			OrderId = orderItem.OrderId;
			NoVAT = orderItem.NoVAT;
			SPCSAccountNumber = orderItem.SPCSAccountNumber;
		}

		[DataMember] public int Id { get; set; }
		[DataMember] public int ArticleId { get; set; }
		[DataMember] public string ArticleDisplayName { get; set; }
		[DataMember] public float SinglePrice { get; set; }
		[DataMember] public int NumberOfItems { get; set; }
		[DataMember] public string Notes { get; set; }
		[DataMember] public string ArticleDisplayNumber { get; set; }
		[DataMember] public float DisplayTotalPrice { get; set; }
		[DataMember] public int OrderId { get; set; }
		[DataMember] public bool NoVAT { get; set; }
		[DataMember] public string SPCSAccountNumber { get; set; }
	}
	
	[DataContract]
	public class OrderData : IOrder{

		public OrderData(IOrder order) {
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
			//RSTId = order.RSTId;
			RstText = order.RstText;
			SalesPersonShopId = order.SalesPersonShopId;
			StatusId = order.StatusId;
			UpdateDate = order.UpdateDate;
			SalesPersonMemberId = order.SalesPersonMemberId;
		}

		[DataMember] public int CompanyId { get; set; }
		[DataMember] public string CompanyUnit { get; set; }
		[DataMember] public DateTime CreatedDate { get; set; }
		[DataMember] public string CustomerFirstName { get; set; }
		[DataMember] public string CustomerLastName { get; set; }
		[DataMember] public string Email { get; set; }
		[DataMember] public int Id { get; set; }
		[DataMember] public long InvoiceNumber { get; set; }
		[DataMember] public bool MarkedAsPayedByShop { get; set; }
		[DataMember] public string PersonalIdNumber { get; set; }
		[DataMember] public string Phone { get; set; }
		//[DataMember] public int RSTId { get; set; }
		[DataMember] public string RstText { get; set; }
		[DataMember] public int SalesPersonShopId { get; set; }
		[DataMember] public int StatusId { get; set; }
		[DataMember] public DateTime UpdateDate { get; set; }
		[DataMember] public int SalesPersonMemberId { get; set; }
		[DataMember] public List<OrderItemData> OrderItems { get; set; }
		[DataMember] public ShopData SellingShop { get; set; }
		[DataMember] public ContractCompanyData ContractCompany{ get; set; }
	}
	
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
	}

	[DataContract]
	public class ContractCompanyData : ICompany {

		public ContractCompanyData(ICompany company) {
			Id = company.Id;
			ContractId = company.ContractId;
			Name = company.Name;
			PostBox = company.PostBox;
			StreetName = company.StreetName;
			Zip = company.Zip;
			City = company.City;
			SPCSCompanyCode = company.SPCSCompanyCode;
		}

		[DataMember] public int Id { get; set; }
		[DataMember] public int ContractId { get; set; }
		[DataMember] public string Name { get; set; }
		[DataMember] public string PostBox { get; set; }
		[DataMember] public string StreetName { get; set; }
		[DataMember] public string Zip { get; set; }
		[DataMember] public string City { get; set; }
		[DataMember] public string SPCSCompanyCode { get; set; }
		[DataMember] public string BankCode { get; set; }
	}

	[DataContract]
	public enum LogTypeData {
		[EnumMember( )]
		Error = 1,
		[EnumMember( )]
		Information = 2,
		[EnumMember( )]
		Other = 3

	}

	[DataContract]
	public class InvoiceStatusData : IInvoiceStatus {
		public InvoiceStatusData(IInvoiceStatus invoiceStatus) {
			InvoiceNumber = invoiceStatus.InvoiceNumber;
			InvoiceCanceled = invoiceStatus.InvoiceCanceled;
			InvoicePaymentCanceled = invoiceStatus.InvoicePaymentCanceled;
			InvoicePaymentDate = invoiceStatus.InvoicePaymentDate;
			Status = invoiceStatus.Status;
			Other = invoiceStatus.Other;
		}
		//public InvoiceStatusData(double invoiceNumber) { InvoiceNumber = invoiceNumber; }
		[DataMember] public long InvoiceNumber { get; set; }
		[DataMember] public bool InvoiceCanceled { get; set; }
		[DataMember] public bool InvoicePaymentCanceled { get; set; }
		[DataMember] public DateTime InvoicePaymentDate { get; set; }
		[DataMember] public string Status { get; set; }
		[DataMember] public object Other { get; set; }
		public bool InvoiceIsPayed {
			get {
				if(InvoiceCanceled)
					return false;
				if(InvoicePaymentCanceled)
					return false;
				return (InvoicePaymentDate > DateTime.MinValue);
			}
		}
	}

	[DataContract]
	public class SynologenWebserviceException : Exception {
		public SynologenWebserviceException(string message) : base(message) { }
		public SynologenWebserviceException(string message, Exception innerException) : base(message, innerException) { }
	}
}