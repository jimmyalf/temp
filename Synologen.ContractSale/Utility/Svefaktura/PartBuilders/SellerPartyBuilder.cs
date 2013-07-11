using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class SellerPartyBuilder : PartBuilderBase, ISvefakturaPartBuilder
    {
        public SellerPartyBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter) : base(settings, formatter) { }

        public virtual void Build(IOrder order, SFTIInvoiceType invoice)
        {
            invoice.SellerParty = new SFTISellerPartyType { Party = GetParty(Settings) };
        }

        protected SFTIPartyType GetParty(ISvefakturaConversionSettings settings)
        {
            return new SFTIPartyType
            {
                PartyName = GetPartyName(settings, x => x.SellingOrganizationName),
                Address = settings.Adress,
                Contact = GetAccountContactFormatted(settings.Contact),
                PartyTaxScheme = GetTaxScheme(Settings),
                PartyIdentification = GetPartyIdentification(settings, x => x.SellingOrganizationNumber)
            };
        }

        protected List<SFTIPartyTaxSchemeType> GetTaxScheme(ISvefakturaConversionSettings settings)
        {
            return new List<SFTIPartyTaxSchemeType>
            {
                new SFTIPartyTaxSchemeType
                {
                    CompanyID = GetIdentifier(x => x.TaxAccountingCode, Formatter.FormatTaxAccountingCode),
                    RegistrationName = new RegistrationNameType { Value = settings.SellingOrganizationName },
                    RegistrationAddress = settings.Adress,
                    TaxScheme = GetTaxScheme("VAT")
                },
                new SFTIPartyTaxSchemeType
                {
                    CompanyID = GetIdentifier(x => x.SellingOrganizationNumber, Formatter.FormatOrganizationNumber),
                    ExemptionReason = new ReasonType { Value = settings.ExemptionReason },
                    RegistrationAddress = settings.RegistrationAdress,
                    TaxScheme = GetTaxScheme("SWT")
                }
            };
        }

        protected virtual SFTITaxSchemeType GetTaxScheme(string id)
        {
            return new SFTITaxSchemeType { ID = new IdentifierType { Value = id } };
        }
    }
}