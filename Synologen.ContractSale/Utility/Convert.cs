using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing{
	public static partial class Convert {

		public static Invoice ToEDIInvoice(EDIConversionSettings EDISettings, IOrder order) {
			var invoiceValueIncludingVAT = System.Convert.ToSingle(order.InvoiceSumIncludingVAT);
			var invoiceValueExcludingVAT = System.Convert.ToSingle(order.InvoiceSumExcludingVAT);
			var interchangeHeader = new InterchangeHeader {RecipientId = order.ContractCompany.EDIRecipientId, SenderId = EDISettings.SenderId};
			var invoiceExpieryDate = interchangeHeader.DateOfPreparation.AddDays(order.ContractCompany.PaymentDuePeriod);
			var invoice = new Invoice(EDISettings.VATAmount, EDISettings.NumberOfDecimalsUsedAtRounding, invoiceValueIncludingVAT, invoiceValueExcludingVAT)
			{
				Articles = ToEDIArticles(order.OrderItems, order /*, order.ContractCompany*/),
				Buyer = GetBuyerInformation(order.ContractCompany.EDIRecipientId, order.ContractCompany),
				BuyerOrderNumber = String.Empty,
				BuyerRSTNumber = order.RstText,
				DocumentNumber = order.InvoiceNumber.ToString(),
				InterchangeHeader = interchangeHeader,
				InvoiceCreatedDate = order.CreatedDate,
				InvoiceSetting = new InvoiceSetting {InvoiceCurrency = EDISettings.InvoiceCurrencyCode, InvoiceExpiryDate = invoiceExpieryDate},
				VendorOrderNumber = order.Id.ToString(),
				Supplier = GetSupplierInformation(EDISettings.SenderId, EDISettings.BankGiro, EDISettings.Postgiro, order.SellingShop)
			};
			return invoice;
		}

		public static SFTIInvoiceType ToSvefakturaInvoice(SvefakturaConversionSettings settings, IOrder order) {
			var invoice = new SFTIInvoiceType();
			TryAddSellerParty(invoice, settings, order.SellingShop);
			TryAddBuyerParty(invoice, order.ContractCompany, order);
			TryAddPaymentMeans(invoice, settings.BankGiro, settings.BankgiroBankIdentificationCode, order.ContractCompany, settings);
			TryAddPaymentMeans(invoice, settings.Postgiro, settings.PostgiroBankIdentificationCode, order.ContractCompany, settings);
			TryAddInvoiceLines(settings, invoice, order.OrderItems, settings.VATAmount);
			TryAddGeneralInvoiceInformation(invoice, settings, order, order.OrderItems /*, order.ContractCompany*/);
			TryAddPaymentTerms(invoice, settings, order.ContractCompany);
			return invoice;
		}

		public static bool AllAreNullOrEmpty(params object[] args) {
			foreach (var value in args){
				if(value == null) continue;
				if(value is string && HasNotBeenSet(value as string)) continue;
				if(value is decimal? && HasNotBeenSet(value as decimal?)) continue;
				return false;
			}
			return true;
		}
		public static bool HasNotBeenSet(string value) { return String.IsNullOrEmpty(value); }
		public static bool HasNotBeenSet(decimal? value) { return !value.HasValue; }
		public static bool HasNotBeenSet(DateTime value) { return value.Equals(DateTime.MinValue); }
		public static bool OneOrMoreHaveValue(params object[] args) {
			return !AllAreNullOrEmpty(args);
		}
	}
}