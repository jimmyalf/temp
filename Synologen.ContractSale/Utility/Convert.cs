using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing
{
	public static partial class Convert 
    {
		public static Invoice ToEDIInvoice(EDIConversionSettings ediSettings, IOrder order) 
        {
			var invoiceValueIncludingVAT = System.Convert.ToSingle(order.InvoiceSumIncludingVAT);
			var invoiceValueExcludingVAT = System.Convert.ToSingle(order.InvoiceSumExcludingVAT);
			var interchangeHeader = new InterchangeHeader {RecipientId = order.ContractCompany.EDIRecipientId, SenderId = ediSettings.SenderId};
			var invoiceExpieryDate = interchangeHeader.DateOfPreparation.AddDays(order.ContractCompany.PaymentDuePeriod);
			var invoice = new Invoice(ediSettings.VATAmount, ediSettings.NumberOfDecimalsUsedAtRounding, invoiceValueIncludingVAT, invoiceValueExcludingVAT)
			{
				Articles = ToEDIArticles(order.OrderItems, order),
				Buyer = GetBuyerInformation(order.ContractCompany.EDIRecipientId, order.ContractCompany),
				BuyerOrderNumber = string.Empty,
				BuyerRSTNumber = order.RstText,
				DocumentNumber = order.InvoiceNumber.ToString(),
				InterchangeHeader = interchangeHeader,
				InvoiceCreatedDate = order.CreatedDate,
				InvoiceSetting = new InvoiceSetting {InvoiceCurrency = ediSettings.InvoiceCurrencyCode, InvoiceExpiryDate = invoiceExpieryDate},
				VendorOrderNumber = order.Id.ToString(),
				Supplier = GetSupplierInformation(ediSettings.SenderId, ediSettings.BankGiro, ediSettings.Postgiro, order.SellingShop)
			};
			return invoice;
		}

        public static SFTIInvoiceType ToSvefakturaInvoice_Old(SvefakturaConversionSettings settings, IOrder order)
        {
            var invoice = new SFTIInvoiceType();
            Svefaktura_Convert_Old.TryAddSellerParty(invoice, settings, order.SellingShop);
            Svefaktura_Convert_Old.TryAddBuyerParty(invoice, order.ContractCompany, order);
            Svefaktura_Convert_Old.TryAddPaymentMeans(invoice, settings.BankGiro, settings.BankgiroBankIdentificationCode, order.ContractCompany, settings);
            Svefaktura_Convert_Old.TryAddPaymentMeans(invoice, settings.Postgiro, settings.PostgiroBankIdentificationCode, order.ContractCompany, settings);
            Svefaktura_Convert_Old.TryAddInvoiceLines(settings, invoice, order.OrderItems, settings.VATAmount);
            Svefaktura_Convert_Old.TryAddGeneralInvoiceInformation(invoice, settings, order, order.OrderItems);
            Svefaktura_Convert_Old.TryAddPaymentTerms(invoice, settings, order.ContractCompany);
            return invoice;
        }

        // Replaced old static building of invoice with a more flexible solution
        public static SFTIInvoiceType ToSvefakturaInvoice(SvefakturaConversionSettings settings, IOrder order, SvefakturaFormatter formatter = null)
        {
            var invoice = new SFTIInvoiceType();
            formatter = formatter ?? new SvefakturaFormatter();
            new SellerPartyBuilder(settings, formatter).Build(order, invoice);
            new BuyerPartyBuilder(settings, formatter).Build(order, invoice);
            new PaymentMeansBuilder(settings, formatter).Build(order, invoice);
            new InvoiceLinesBuilder(settings, formatter).Build(order, invoice);
            new InvoiceInformationBuilder(settings, formatter).Build(order, invoice);
            new PaymentTermsBuilder(settings, formatter).Build(order, invoice);
            return invoice;
        }
	}
}