using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Data.Types;

namespace Spinit.Wpc.Synologen.Utility {
	public class CommonConversion {
		public static IList<string> GetFreeTextRows(CompanyRow company, OrderRow order) {
			if (String.IsNullOrEmpty(company.InvoiceFreeTextFormat)) return new List<string>();
			var parsedInvoiceFreeText = ParseInvoiceFreeTeext(company.InvoiceFreeTextFormat, order);
			return parsedInvoiceFreeText.Trim().Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
		}
		public static string GetFreeTextRowsAsString(CompanyRow company, OrderRow order) {
			return String.IsNullOrEmpty(company.InvoiceFreeTextFormat) ? null : ParseInvoiceFreeTeext(company.InvoiceFreeTextFormat, order);
		}

		public static string ParseInvoiceFreeTeext(string invoiceFreeTextFormat, OrderRow order) {
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerName}", order.CustomerCombinedName ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerPersonalIdNumber}", order.PersonalIdNumber ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CompanyUnit}", order.CompanyUnit ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerPersonalBirthDateString}", order.PersonalBirthDateString ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerFirstName}", order.CustomerFirstName ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerLastName}", order.CustomerLastName ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{BuyerCompanyId}", order.CompanyId.ToString() ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{RST}", order.RstText ?? String.Empty);
			return invoiceFreeTextFormat;
		}



	}
}