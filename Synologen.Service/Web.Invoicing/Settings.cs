using System;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Synologen.Service.Web.Invoicing.ConfigurationSettings;

namespace Synologen.Service.Web.Invoicing
{
    public static class Settings
    {
        public static SvefakturaConversionSettings GetSvefakturaSettings()
        {
            var config = new WebServiceConfiguration();
            return new SvefakturaConversionSettings
            {
                InvoiceIssueDate = DateTime.Now,
                BankGiro = config.BankGiro,
                Postgiro = config.Postgiro,
                VATAmount = (decimal)config.VATAmount,
                BankgiroBankIdentificationCode = config.BankGiroCode,
                PostgiroBankIdentificationCode = config.PostGiroCode,
                ExemptionReason = config.ExemptionReason,
                InvoiceCurrencyCode = (CurrencyCodeContentType)config.CurrencyCodeId,
                InvoiceExpieryPenaltySurchargePercent = config.InvoiceExpieryPenaltySurchargePercent,
                InvoicePaymentTermsTextFormat = config.InvoicePaymentTermsTextFormat,
                InvoiceTypeCode = config.SvefakturaInvoiceTypeCode,
                Adress = new SFTIAddressType
                {
                    CityName = new CityNameType { Value = config.SellingOrganizationCity },
                    Country = GetSellingOrganizationCountry(),
                    PostalZone = new ZoneType { Value = config.SellingOrganizationPostalCode },
                    Postbox = config.SellingOrganizationPostBox
                        .TryGetValue<PostboxType>((postbox, value) => postbox.Value = value),
                    StreetName = new StreetNameType { Value = config.SellingOrganizationStreetName }
                },
                RegistrationAdress = new SFTIAddressType
                {
                    CityName = config.SellingOrganizationRegistrationCity.TryGetValue<CityNameType>((city, value) => city.Value = value),
                    Country = GetSellingOrganizationCountry(),
                },
                Contact = new SFTIContactType
                {
                    ElectronicMail = new MailType { Value = config.SellingOrganizationContactEmail },
                    Name = new NameType { Value = config.SellingOrganizationContactName },
                    Telefax = new TelefaxType { Value = config.SellingOrganizationFax },
                    Telephone = new TelephoneType { Value = config.SellingOrganizationTelephone }
                },
                SellingOrganizationName = config.SellingOrganizationName,
                SellingOrganizationNumber = config.SellingOrganizationNumber,
                TaxAccountingCode = config.TaxAccountingCode,
                VATFreeReasonMessage = config.VATFreeReasonMessage,
                EDIAddress = config.EDISenderId
            };
        }

        private static SFTICountryType GetSellingOrganizationCountry()
        {
            var config = new WebServiceConfiguration();
            return new SFTICountryType
            {
                IdentificationCode = new CountryIdentificationCodeType
                {
                    Value = (CountryIdentificationCodeContentType)config.SellingOrganizationCountryCode,
                    name = config.SellingOrganizationCountryName
                }
            };
        }

        public static EDIConversionSettings GetEDISetting()
        {
            var config = new WebServiceConfiguration();
            return new EDIConversionSettings
            {
                BankGiro = config.BankGiro,
                Postgiro = config.Postgiro,
                SenderEdiAddress = config.EDISenderId,
                VATAmount = config.VATAmount,
                InvoiceCurrencyCode = config.InvoiceCurrencyCode,
                NumberOfDecimalsUsedAtRounding = 2,
            };
        }

        public static PDF_OrderInvoiceConversionSettings GetPDF_OrderInvoiceSettings()
        {
            var config = new WebServiceConfiguration();
            return new PDF_OrderInvoiceConversionSettings
            {
                EmailAdminAddress = config.EmailAdminAddress,
                EmailSynologenInvoiceSender = config.EmailSynologenInvoiceSender,
                EmailSpinitServiceAddress = config.EmailSpinitServiceAddress,
                EmailSpinitServiceSendUser = config.EmailSpinitServiceSendUser,
                EmailSpinitServicePassword = config.EmailSpinitServicePassword,
                PDF_OrderInvoiceDebugMode = config.PDF_OrderInvoiceDebugMode
            };
        }
    }
}