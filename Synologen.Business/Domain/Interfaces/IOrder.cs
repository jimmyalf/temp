using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IOrder 
	{
		[DataMember] int CompanyId{ get; set; }
		[DataMember] string CompanyUnit { get; set; }
		[DataMember] DateTime CreatedDate { get; set; }
		[DataMember] string CustomerFirstName { get; set; }
		[DataMember] string Email { get; set; }
		[DataMember] string CustomerLastName { get; set; }
		[DataMember] int Id { get; set; }
		[DataMember] long InvoiceNumber { get; set; }
		[DataMember] bool MarkedAsPayedByShop { get; set; }
		[DataMember] string PersonalIdNumber { get; set; }
		[DataMember] string Phone { get; set; }
		[DataMember] string RstText { get; set; }
		[DataMember] int SalesPersonShopId { get; set; }
		[DataMember] int StatusId { get; set; }
		[DataMember] DateTime UpdateDate { get; set; }
		[DataMember] int SalesPersonMemberId { get; set; }
		[DataMember] double InvoiceSumIncludingVAT { get; set; }
		[DataMember] double InvoiceSumExcludingVAT { get; set; }
		[DataMember] string CustomerOrderNumber { get; set; }
		[DataMember] string CustomerCombinedName { get; }
		[DataMember] string PersonalBirthDateString { get; }
		[DataMember] IList<OrderItem> OrderItems { get; set; }
		[DataMember] Shop SellingShop { get; set; }
		[DataMember] Company ContractCompany{ get; set; }
		DateTime? InvoiceDate { get; }
		string ParseFreeText();
	}
}