using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.WebForms;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Reports.Invoicing
{
    public class InvoiceReportViewService : IInvoiceReportViewService
    {
        private const string PaymentTermsText = "{InvoiceNumberOfDueDays} dagar netto";

        private const string NewLine = "\r\n";

        public ReportDataSource[] GetInvoiceReportDataSources(Order invoice)
        {
            var invoiceReport = GetInvoiceReport(invoice);
            var invoiceRows = invoice.OrderItems.Select(GetInvoiceRow).ToList();
            var invoiceReportDataSource = new ReportDataSource(
                "InvoiceReport", new List<InvoiceCopyReport> { invoiceReport });
            var invoiceRowsDataSource = new ReportDataSource("InvoiceRow", invoiceRows);
            return new[] { invoiceReportDataSource, invoiceRowsDataSource };
        }

        public ReportDataSource[] GetCreditInvoiceReportDataSources(Order invoice, string creditInvoiceNumber)
        {
            var invoiceReport = GetInvoiceCreditReport(invoice, creditInvoiceNumber);
            var invoiceRows = invoice.OrderItems.Select(GetInvoiceRowInverted).ToList();
            var invoiceReportDataSource = new ReportDataSource(
                "InvoiceReport", new List<InvoiceCreditReport> { invoiceReport });
            var invoiceRowsDataSource = new ReportDataSource("InvoiceRow", invoiceRows);
            return new[] { invoiceReportDataSource, invoiceRowsDataSource };
        }

        private static InvoiceCopyReport GetInvoiceReport(IOrder order)
        {
            var report = new InvoiceCopyReport
            {
                Id = order.Id,
                InvoiceFreeText = order.ParseFreeText(),
                LineExtensionsTotalAmount = order.InvoiceSumExcludingVAT.ToString("N2"),
                TotalTaxAmount = (order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT).ToString("N2"),
                TaxInclusinveTotalAmount = order.InvoiceSumIncludingVAT.ToString("N2"),
                ShopContactText = GetShopInvoiceContaxtText(order.SellingShop),
                PaymentTermsNote =
                    PaymentTermsText.ReplaceWith(
                        new { InvoiceNumberOfDueDays = order.ContractCompany.PaymentDuePeriod }),
                InvoiceRecipientOrderNumber = order.CustomerOrderNumber ?? string.Empty,
                InvoiceNumber = order.InvoiceNumber.ToString(),
                OrderCreatedDate = order.CreatedDate.ToString("yyyy-MM-dd"),
                InvoiceDate = GetInvoiceDate(order),
                InvoiceDueDate = GetInvoiceDueDate(order),
            };
            report.SetInvoiceRecipient(order.ContractCompany, order);
            return report;
        }

        private static InvoiceCreditReport GetInvoiceCreditReport(IOrder order, string invoiceCreditNumber)
        {
            var report = new InvoiceCreditReport
            {
                Id = order.Id,
                InvoiceFreeText = order.ParseFreeText(),
                LineExtensionsTotalAmount = (order.InvoiceSumExcludingVAT * -1).ToString("N2"),
                TotalTaxAmount = ((order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT) * -1).ToString("N2"),
                TaxInclusinveTotalAmount = (order.InvoiceSumIncludingVAT * -1).ToString("N2"),
                ShopContactText = GetShopInvoiceContaxtText(order.SellingShop),
                PaymentTermsNote =
                    PaymentTermsText.ReplaceWith(
                        new { InvoiceNumberOfDueDays = order.ContractCompany.PaymentDuePeriod }),
                InvoiceRecipientOrderNumber = order.CustomerOrderNumber ?? string.Empty,
                InvoiceNumber = order.InvoiceNumber.ToString(),
                OrderCreatedDate = order.CreatedDate.ToString("yyyy-MM-dd"),
                InvoiceDate = GetInvoiceDate(order),
                InvoiceDueDate = GetInvoiceDueDate(order),
                InvoiceCreditNumber = invoiceCreditNumber
            };
            report.SetInvoiceRecipient(order.ContractCompany, order);
            return report;
        }

        private static string GetInvoiceDueDate(IOrder order)
        {
            return order.InvoiceDate.HasValue && order.ContractCompany != null
                       ? order.InvoiceDate.Value.AddDays(order.ContractCompany.PaymentDuePeriod).ToString("yyyy-MM-dd")
                       : "N/A";
        }

        private static string GetInvoiceDate(IOrder order)
        {
            return order.InvoiceDate.HasValue ? order.InvoiceDate.Value.ToString("yyyy-MM-dd") : "N/A";
        }

        private static string GetShopInvoiceContaxtText(IShop shop)
        {
            return shop.Format("{Name} ({OrganizationNumber}){NewLine}{AddressLine}{NewLine}{ZipAndCity}");
        }

        //private static IEnumerable<InvoiceRow> GetInvoiceRows(IEnumerable<OrderItem> orderItems)
        //{
        //    //// Invertera kostnader för att kunna kreditera fakturan
        //    //foreach (var orderItem in invoice.OrderItems)
        //    //{
        //    //    orderItem.DisplayTotalPrice *= -1;
        //    //    orderItem.SinglePrice *= -1;
        //    //}

        //    //invoice.InvoiceSumExcludingVAT *= -1;
        //    //invoice.InvoiceSumIncludingVAT *= -1;

        //    Func<OrderItem, InvoiceRow> parseInvoiceRow = orderItem => new InvoiceRow
        //    {
        //        Description = orderItem.ArticleDisplayName, 
        //        Quantity = orderItem.NumberOfItems.ToString("N2"), 
        //        SinglePrice = orderItem.SinglePrice.ToString("N2"), 
        //        RowAmount = orderItem.DisplayTotalPrice.ToString("N2")
        //    };
        //    return orderItems.Select(parseInvoiceRow);
        //}

        private static InvoiceRow GetInvoiceRow(OrderItem orderItem)
        {
            return new InvoiceRow
            {
                Description = orderItem.ArticleDisplayName,
                Quantity = orderItem.NumberOfItems.ToString("N2"),
                SinglePrice = orderItem.SinglePrice.ToString("N2"),
                RowAmount = orderItem.DisplayTotalPrice.ToString("N2")
            };
        }

        private static InvoiceRow GetInvoiceRowInverted(OrderItem orderItem)
        {
            return new InvoiceRow
            {
                Description = orderItem.ArticleDisplayName,
                Quantity = orderItem.NumberOfItems.ToString("N2"),
                SinglePrice = (orderItem.SinglePrice * -1).ToString("N2"),
                RowAmount = (orderItem.DisplayTotalPrice * -1).ToString("N2")
            };
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