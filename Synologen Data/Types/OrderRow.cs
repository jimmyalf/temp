using System;
using Spinit.Wpc.Synologen.Business.Interfaces;

namespace Spinit.Wpc.Synologen.Data.Types {
	public class OrderRow : IOrder {

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
		//public int RSTId { get; set; }
		public string RstText { get; set; }
		public int SalesPersonShopId { get; set; }
		public int StatusId { get; set; }
		public DateTime UpdateDate { get; set; }
		public int SalesPersonMemberId { get; set; }

		public double InvoiceSumIncludingVAT { get; set; }
		public double InvoiceSumExcludingVAT { get; set; }
		public string CustomerOrderNumber { get; set; }
		public decimal? RoundOffAmount { get; set; }
		public string CustomerCombinedName {
			get { return String.Concat( CustomerFirstName ?? String.Empty,  " ",  CustomerLastName ?? String.Empty ).Trim(); }
		}
		public string PersonalBirthDateString {
			get {
				if(PersonalIdNumber == null) return null;
				return (PersonalIdNumber.Length < 12) ? null : PersonalIdNumber.Substring(0, 8);
			}
		}
	}
}