using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Reports.Models
{
	public class InvoiceCopyReport
	{
		private const string InvoiceRecipientFormat = "{Name}{NewLine}{Department}{NewLine}{PostBox}{NewLine}{Street}{NewLine}{CountryID} - {PostalCode} {City}{NewLine}{CountryName}";

		public int Id { get; set; }
		public string InvoiceRecipient { get; set; }
		public string InvoiceFreeText { get; set; }
		public string LineExtensionsTotalAmount { get; set; }
		public string TotalTaxAmount { get; set; }
		public string TaxInclusinveTotalAmount { get; set; }
		public string ShopContactText { get; set; }
		public string PaymentTermsNote { get; set; }
		public string InvoiceRecipientOrderNumber { get; set; }
		public string InvoiceNumber { get; set; }
		public string InvoiceDate { get; set; }
		public string InvoiceDueDate { get; set; }
		public string OrderCreatedDate { get; set; }

		public static IList<InvoiceCopyReport> GetList()
		{
			return new List<InvoiceCopyReport>();
		}

		public void SetInvoiceRecipient(ICompany company, IOrder order)
		{
			InvoiceRecipient = InvoiceRecipientFormat.ReplaceWith(new
			{
				Name = company.Return(x => x.InvoiceCompanyName, string.Empty),
				Department = order.Return(x => x.CompanyUnit, string.Empty),
				PostBox = company.Return(x => x.PostBox, string.Empty),
				Street = company.Return(x => x.StreetName, string.Empty),
				CountryID = "SE",
				PostalCode = company.Return(x => x.Zip, string.Empty),
				City = company.Return(x => x.City, string.Empty),
				CountryName = company.With(x => x.Country).Return(x => x.Name, string.Empty),
				NewLine = "\r\n"
			}).RegexReplace("(\r\n){2,}", "\r\n");
		}
	}
}
