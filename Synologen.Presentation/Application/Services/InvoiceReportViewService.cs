using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			const string paymentTermsText = "{InvoiceNumberOfDueDays} dagar netto";
			return new InvoiceReport 
			{
				Id = order.Id, 
				InvoiceFreeText = order.ParseFreeText(), 
				LineExtensionsTotalAmount = order.InvoiceSumExcludingVAT.ToString("N2"), 
				TotalTaxAmount = (order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT).ToString("N2"), 
				TaxInclusinveTotalAmount = order.InvoiceSumIncludingVAT.ToString("N2"),  
				ShopContactText = GetShopContaxtText(order.SellingShop),
				PaymentTermsNote = paymentTermsText.ReplaceWith(new {InvoiceNumberOfDueDays = order.ContractCompany.PaymentDuePeriod}),
				InvoiceRecipientOrderNumber = order.CustomerOrderNumber ?? string.Empty,
				InvoiceNumber = order.InvoiceNumber.ToString("N0"),
				OrderCreatedDate = order.CreatedDate.ToString("yyyy-MM-dd"),
				InvoiceDate = GetInvoiceDate(order),
                InvoiceDueDate = GetInvoiceDueDate(order),
			}.SetInvoiceRecipient(order.ContractCompany, order);
		}

		private static string GetInvoiceDueDate(IOrder order)
		{
			return order.InvoiceDate.HasValue && order.ContractCompany != null
				? order.InvoiceDate.Value.AddDays(order.ContractCompany.PaymentDuePeriod).ToString("yyyy-MM-dd") 
				: "N/A";
		}

		private static string GetInvoiceDate(IOrder order)
		{
			return order.InvoiceDate.HasValue 
				? order.InvoiceDate.Value.ToString("yyyy-MM-dd") 
				: "N/A";
		}


		private static string GetShopContaxtText(IShop shop)
		{
			const string shopContactText = "{ShopName} ({OrganizationNumber}){NewLine}{Address}{NewLine}{TelephoneAndEmail}";
			return shopContactText.ReplaceWith(new
			{
				NewLine, 
				ShopName = shop.Name, 
				shop.OrganizationNumber, 
				Address = GetShopAddress(shop), 
				TelephoneAndEmail = (shop.Phone + " " + shop.Email).Trim()
			});
		}

		private static string GetShopAddress(IShop shop)
		{
			return " ".AsDelimiter().ConcatenateFieldsFor(shop, 
				x => x.Address, 
				x => x.Address2, 
				x => x.Zip, 
				x => x.City);
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

	public class DelimiterOption
	{
		public string Delimiter { get; set; }

		public DelimiterOption(string delimiter)
		{
			Delimiter = delimiter;
		}

		public string ConcatenateFields(params string[] parameters)
		{
			var output = string.Empty;
			var nonEmptyParameters = parameters.Where(x => !x.IsNullOrEmpty());
			foreach (var parameter in nonEmptyParameters)
			{
				if(output.IsNullOrEmpty())
				{
					output = parameter;
				}
				else
				{
					output += Delimiter + parameter;
				}
			}
			return output;
		}

		public string ConcatenateFieldsFor<TType>(TType value, params Func<TType,string>[] parameters)
		{
			var evaluatedParameters = parameters.Select(func => func(value));
			return ConcatenateFields(evaluatedParameters.ToArray());
		}

	}

	public static class DelimiterOptionsExtensions
	{
		public static DelimiterOption AsDelimiter(this string delimiter)
		{
			return new DelimiterOption(delimiter);
		}
	}
}