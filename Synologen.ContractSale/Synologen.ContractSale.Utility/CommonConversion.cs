using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Invoicing{
	public class CommonConversion {
		public static IList<string> GetFreeTextRows(ICompany company, IOrder order) {
			if (String.IsNullOrEmpty(company.InvoiceFreeTextFormat)) return new List<string>();
			var parsedInvoiceFreeText = ParseInvoiceFreeTeext(company.InvoiceFreeTextFormat, order, company);
			return parsedInvoiceFreeText.Trim().Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
		}
		public static string GetFreeTextRowsAsString(ICompany company, IOrder order) {
			return String.IsNullOrEmpty(company.InvoiceFreeTextFormat) ? null : ParseInvoiceFreeTeext(company.InvoiceFreeTextFormat, order, company);
		}

		public static string ParseInvoiceFreeTeext(string invoiceFreeTextFormat, IOrder order, ICompany company) {
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerName}", order.CustomerCombinedName ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerPersonalIdNumber}", order.PersonalIdNumber ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CompanyUnit}", order.CompanyUnit ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerPersonalBirthDateString}", order.PersonalBirthDateString ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerFirstName}", order.CustomerFirstName ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerLastName}", order.CustomerLastName ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{BuyerCompanyId}", order.CompanyId.ToString() ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{RST}", order.RstText ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{BankCode}", company.BankCode ?? String.Empty);
			if(order.SellingShop!=null){
				invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{SellingShopName}", order.SellingShop.Name ?? String.Empty);
				invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{SellingShopNumber}", order.SellingShop.Number ?? String.Empty);
			}
			return invoiceFreeTextFormat;
		}



	}
}