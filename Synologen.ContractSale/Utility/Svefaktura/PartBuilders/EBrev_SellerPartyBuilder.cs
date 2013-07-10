using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class EBrev_SellerPartyBuilder : PartBuilderBase, ISvefakturaPartBuilder
    {
        public EBrev_SellerPartyBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter) : base(settings, formatter) { }

        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
            invoice.SellerParty = new SFTISellerPartyType
            {
                Party = GetParty(order.SellingShop),
                AccountsContact = GetAccountContact(Settings.Contact)
            };
        }

        protected SFTIContactType GetAccountContact(SFTIContactType contact)
        {
            return Build<SFTIContactType>().With(contact)
                .Fill(x => x.ElectronicMail).Using(x => x.With(a => a.ElectronicMail).Return(a => a.Value, null))
                .Fill(x => x.Name).Using(x => x.With(a => a.Name).Return(a => a.Value, null))
                .Fill(x => x.Telefax).Using(x => x.With(a => a.Telefax).Return(a => a.Value, null), Formatter.FormatPhoneNumber)
                .Fill(x => x.Telephone).Using(x => x.With(a => a.Telephone).Return(a => a.Value, null), Formatter.FormatPhoneNumber)
                .GetEntity();
        }

        protected SFTIPartyType GetParty(IShop shop)
        {
            return new SFTIPartyType
            {
                PartyName = GetPartyName(shop, x => x.Name),
                Address = GetAddress(shop),
                Contact = GetContact(shop),
                PartyTaxScheme = GetTaxScheme(Settings),
                PartyIdentification = GetPartyIdentification(shop, x => x.OrganizationNumber)
            };
        }

        protected SFTIAddressType GetAddress(IShop shop)
        {
            return Build<SFTIAddressType>().With(shop)
                .Fill(x => x.Postbox).Using(x => x.Address)
                .Fill(x => x.StreetName).Using(x => x.Address2)
                .Fill(x => x.PostalZone).Using(x => x.Zip)
                .Fill(x => x.CityName).Using(x => x.City)
                .FillEntity(x => x.Country).Using(GetSwedishCountry())
                .GetEntity();
        }

        protected SFTIContactType GetContact(IShop shop)
        {
            return Build<SFTIContactType>().With(shop)
                .Fill(x => x.ElectronicMail).Using(x => x.Email)
                .Fill(x => x.Name).Using(x => x.ContactCombinedName)
                .Fill(x => x.Telefax).Using(x => x.Fax, Formatter.FormatPhoneNumber)
                .Fill(x => x.Telephone).Using(x => x.Phone, Formatter.FormatPhoneNumber)
                .GetEntity();
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