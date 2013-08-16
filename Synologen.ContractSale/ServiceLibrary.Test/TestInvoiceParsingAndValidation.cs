using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Integration.Services.Test
{
    [TestFixture, Explicit]
    public class TestInvoiceParsingAndValidation
    {
        private SqlProvider _provider;
        private SvefakturaConversionSettings _settings;

        [SetUp]
        public void Setup()
        {
            // const string connectionString = @"Initial Catalog=dbWpcSynologen;Data Source=TEAL;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";
            const string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=dbWpcSynologen;Integrated Security=SSPI;";
            _provider = new SqlProvider(connectionString);
            _settings = GetSettings();
        }

        [Test]
        public void Debugging()
        {
            var invoiceList = new List<SFTIInvoiceType>();
            var orderList = new List<Order>();
            for (var i = 4039; i <= 4045; i++)
            {
                var order = _provider.GetOrder(i);
                orderList.Add(order);
                if (order.StatusId == 3) continue;
                var invoice = General.CreateInvoiceSvefaktura(order, _settings);
                invoiceList.Add(invoice);
            }
            foreach (var invoice in invoiceList)
            {
                //if(invoice.ID.Value != "4599") continue;
                var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
                if (ruleViolations.Any())
                {
                    throw new AssertionException("Ruleviolations were found for invoice " + invoice.ID.Value);
                }
            }
            var encoding = Encoding.GetEncoding("ISO-8859-1");
            var postOfficeHeader = "<?POSTNET SND=\"STREAMS000345\" REC=\"SE00087815000\" MSGTYPE=\"SYNOLOG\"?>";
            var output = SvefakturaSerializer.Serialize(new SFTIInvoiceList { Invoices = invoiceList }, encoding, "\r\n", Formatting.Indented, postOfficeHeader);
            Debug.Write(output);
            var filePath = @"C:\Exempelfaktura_" + DateTime.Now.ToString("yyyy-MM-dd_HH.mm") + ".xml";
            File.WriteAllText(filePath, output, encoding);
        }

        [Test]
        public void Debugging2()
        {
            var order = _provider.GetOrder(4061);
            var invoice = General.CreateInvoiceSvefaktura(order, _settings);
            var ruleViolations = SvefakturaValidator.ValidateObject(invoice).ToList();
            var output = SvefakturaSerializer.Serialize(new SFTIInvoiceList { Invoices = new List<SFTIInvoiceType>(new[] { invoice }) }, Encoding.UTF8, "\r\n", Formatting.Indented, null);
            Debug.Write(output);
        }

        public static SvefakturaConversionSettings GetSettings()
        {
            return new SvefakturaConversionSettings
            {
                SellingOrganizationName = "Synhälsan Svenska AB",
                Adress = new SFTIAddressType
                {
                    StreetName = new StreetNameType { Value = "Strandbergsgatan 61" },
                    CityName = new CityNameType { Value = "Stockholm" },
                    Country = GetSwedishSFTICountryType(),
                    //Postbox = new PostboxType{ Value = "Box 123" },
                    PostalZone = new ZoneType { Value = "112 51" }

                },
                RegistrationAdress = new SFTIAddressType
                {
                    CityName = new CityNameType { Value = "Klippan" },
                    Country = GetSwedishSFTICountryType(),
                },
                Contact = new SFTIContactType
                {
                    ElectronicMail = new MailType { Value = "info@synologen.se" },
                    Name = new NameType { Value = "Violetta Nordlöf" },
                    Telefax = new TelefaxType { Value = "084407359" },
                    Telephone = new TelephoneType { Value = "084407350" }
                },
                SellingOrganizationNumber = "5562626100",
                ExemptionReason = "Innehar F-skattebevis",
                TaxAccountingCode = "SE556262610001",
                InvoiceIssueDate = new DateTime(2009, 10, 30),
                InvoiceTypeCode = "380",
                InvoiceCurrencyCode = CurrencyCodeContentType.SEK,
                VATAmount = 0.25m,
                BankGiro = "56936677",
                BankgiroBankIdentificationCode = "BGABSESS",
                //Postgiro = "123456",
                //PostgiroBankIdentificationCode = "PGSISESS",
                InvoicePaymentTermsTextFormat = "{InvoiceNumberOfDueDays} dagar netto",
                InvoiceExpieryPenaltySurchargePercent = 8m,
                VATFreeReasonMessage = "Momsfri"
            };
        }

        public static SFTICountryType GetSwedishSFTICountryType()
        {
            return new SFTICountryType
            {
                IdentificationCode = new CountryIdentificationCodeType
                {
                    name = "Sverige",
                    Value = CountryIdentificationCodeContentType.SE
                }
            };
        }
    }
}