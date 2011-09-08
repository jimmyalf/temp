using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.WebForms;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.Reports;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class InvoiceReportViewService : IInvoiceReportViewService
	{
		const string NewLine = "\r\n";

		public ReportDataSource[] GetInvoiceReportDataSources(Order invoice)
		{
			var invoiceReport = GetInvoiceReport(invoice, "12,5");
			var invoiceRows = GetInvoiceRows(invoice.OrderItems);
			var invoiceReportDataSource = new ReportDataSource("InvoiceReport", new List<InvoiceReport> {invoiceReport});
			var invoiceRowsDataSource = new ReportDataSource("InvoiceRow", invoiceRows);
			return new[] {invoiceReportDataSource, invoiceRowsDataSource};
		}

		private static InvoiceReport GetInvoiceReport(IOrder order, string penaltyChargePercent)
		{
			const string invoiceText = "Anmärkningar mot denna faktura skall göras inom 8 dgr för att godkännas.{NewLine}Vid betalning efter förfallodagen debiteras dröjsmålsränta med diskonto {PenaltyChargePercent}%.{NewLine}Påminnelseavgift 45 kronor.";
			const string paymentTermsText = "{InvoiceNumberOfDueDays} dagar netto";
			return new InvoiceReport 
			{
				Id = order.Id, 
				InvoiceFreeText = GetInvoiceFreeText(order), 
				LineExtensionsTotalAmount = order.InvoiceSumExcludingVAT.ToString("N2"), 
				TotalTaxAmount = (order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT).ToString("N2"), 
				TaxInclusinveTotalAmount = order.InvoiceSumIncludingVAT.ToString("N2"), 
				InvoiceFooterText = invoiceText.ReplaceWith(new {PenaltyChargePercent = penaltyChargePercent, NewLine}), 
				ShopContactText = GetShopContaxtText(order.SellingShop),
				PaymentTermsNote = paymentTermsText.ReplaceWith(new {InvoiceNumberOfDueDays = order.ContractCompany.PaymentDuePeriod}),
				InvoiceRecipientOrderNumber = order.CustomerOrderNumber ?? string.Empty,
				InvoiceNumber = order.InvoiceNumber.ToString("N0"),
				OrderCreatedDate = order.CreatedDate.ToString("yyyy-MM-dd"),
				InvoiceDate = "N/A",
                InvoiceDueDate = "N/A"
			}.SetInvoiceRecipient(order.ContractCompany, order);
		}

		private static string GetInvoiceFreeText(IOrder order)
		{
			const string invoiceFreeText = "Beställare: {CustomerName}{NewLine}Beställarens födelsedagsdatum: {BirthdayDate}";
			return invoiceFreeText.ReplaceWith(new {CustomerName = order.ParseName(x => x.CustomerFirstName, x => x.CustomerLastName), BirthdayDate = order.PersonalBirthDateString ?? string.Empty, NewLine});
		}

		private static string GetShopContaxtText(IShop shop)
		{
			const string shopContactText = "{ShopName} ({OrganizationNumber}){NewLine}{Address}{NewLine}{Telephone}";
			return shopContactText.ReplaceWith(new
			{
				NewLine, 
				ShopName = shop.Name, 
				OrganizationNumber = "N/A", 
				Address = GetShopAddress(shop), 
				Telephone = shop.Phone
			});
		}

		private static string GetShopAddress(IShop shop)
		{
			Func<string, string, string, string> tryAdd = (input, delimiter, parameterToAdd) =>
			{
				var output = input;
				if (String.IsNullOrEmpty(parameterToAdd)) return output;
				if (!String.IsNullOrEmpty(input)) output += delimiter;
				output += parameterToAdd;
				return output;
			};
			var address = String.Empty;
			address = tryAdd(address, ", ", shop.Address);
			address = tryAdd(address, ", ", shop.Address2);
			address = tryAdd(address, ", ", shop.Zip);
			address = tryAdd(address, " ", shop.City);
			return address;
		}

		private static IEnumerable<InvoiceRow> GetInvoiceRows(IEnumerable<OrderItem> orderItems)
		{
			Func<OrderItem,InvoiceRow> parseInvoiceRow = orderItem => new InvoiceRow
			{
				Description = orderItem.ArticleDisplayName, 
				Quantity = orderItem.NumberOfItems.ToString(), 
				SinglePrice = orderItem.SinglePrice.ToString("N2"), 
				RowAmount = orderItem.DisplayTotalPrice.ToString("N2")
			};
			return orderItems.Select(parseInvoiceRow);
		}
	}
}