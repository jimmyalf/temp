using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Utility {
	public static partial class Convert {

		public static Invoice ToEDIInvoice(EDIConversionSettings EDISettings, OrderRow order, List<IOrderItem> orderItems, CompanyRow company, IShop shop) {
			var invoiceValueIncludingVAT = System.Convert.ToSingle(order.InvoiceSumIncludingVAT);
			var invoiceValueExcludingVAT = System.Convert.ToSingle(order.InvoiceSumExcludingVAT);
			var interchangeHeader = new InterchangeHeader {RecipientId = company.EDIRecipientId, SenderId = EDISettings.SenderId};
			var invoiceExpieryDate = interchangeHeader.DateOfPreparation.AddDays(company.PaymentDuePeriod);
			var invoice = new Invoice(EDISettings.VATAmount, EDISettings.NumberOfDecimalsUsedAtRounding, invoiceValueIncludingVAT, invoiceValueExcludingVAT) {
				Articles = ToEDIArticles(orderItems, order, company),
				Buyer = GetBuyerInformation(company.EDIRecipientId, company),
             	BuyerOrderNumber = String.Empty,
             	BuyerRSTNumber = order.RstText,
             	DocumentNumber = order.InvoiceNumber.ToString(),
				InterchangeHeader = interchangeHeader,
             	InvoiceCreatedDate = order.CreatedDate,
				InvoiceSetting = new InvoiceSetting { InvoiceCurrency = EDISettings.InvoiceCurrencyCode, InvoiceExpiryDate = invoiceExpieryDate },
             	VendorOrderNumber = order.Id.ToString(),
				Supplier = GetSupplierInformation(EDISettings.SenderId, EDISettings.BankGiro,EDISettings.Postgiro, shop)
             };
			return invoice;
		}

		public static SFTIInvoiceType ToSvefakturaInvoice(SvefakturaConversionSettings settings, OrderRow order, List<IOrderItem> orderItems, CompanyRow company, ShopRow shop) {
			var invoice = new SFTIInvoiceType();
			TryAddSellerParty(invoice, settings, shop);
			TryAddBuyerParty(invoice, company, order);
			TryAddPaymentMeans(invoice, settings.BankGiro, settings.BankgiroBankIdentificationCode, company, settings);
			TryAddPaymentMeans(invoice, settings.Postgiro, settings.PostgiroBankIdentificationCode, company, settings);
			TryAddTaxTotal(invoice, settings.VATAmount);
			TryAddGeneralInvoiceInformation(invoice, settings, order);
			TryAddInvoiceLines(invoice, orderItems, settings.VATAmount);
			TryAddPaymentTerms(invoice, settings, company);
			return invoice;
		}

		#region Helper Methods
		private static bool AllAreNullOrEmpty(params object[] args) {
			foreach (var value in args){
				if(value == null) continue;
				if(value.GetType().Equals(typeof(string))	&& IsNullOrEmpty((string)value))	continue;
				if(value.GetType().Equals(typeof(decimal?)) && IsNullOrEmpty((decimal?)value)) continue;
				return false;
			}
			return true;
		}

		private static bool IsNullOrEmpty(string value) { return String.IsNullOrEmpty(value); }
		private static bool IsNullOrEmpty(decimal? value) { return !value.HasValue; }

		private static bool OneOrMoreHaveValue(params object[] args) {
			return !AllAreNullOrEmpty(args);
		}

		#endregion

	}
}