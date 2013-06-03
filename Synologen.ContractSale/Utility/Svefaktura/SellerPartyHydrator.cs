using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Helpers;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using NameType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura
{
    public class SellerPartyHydrator : SvefakturaHydratorBase, ISvefakturaHydrator
    {
        public SellerPartyHydrator(SvefakturaConversionSettings settings, SvefakturaFormatter formatter) : base(settings, formatter) { }

        public void Fill(IOrder order, SFTIInvoiceType invoice)
        {
            invoice.SellerParty = new SFTISellerPartyType
            {
                Party = GetParty(order.SellingShop),
                AccountsContact = Settings.Contact
            };
        }

        protected SFTIPartyType GetParty(IShop shop)
        {
            return new SFTIPartyType
            {
                PartyName = GetPartyName(shop),
                Address = GetAddress(shop),
                Contact = GetContact(shop),
                PartyTaxScheme = GetTaxScheme(Settings),
                PartyIdentification = GetPartyIdentification(shop)
            };
        }

        protected List<NameType> GetPartyName(IShop shop)
        {
            return new List<NameType>
            {
                new NameType { Value = shop.Return(x => x.Name, null) }
            };
        }

        protected SFTIAddressType GetAddress(IShop shop)
        {
            return new EntityFiller<IShop, SFTIAddressType>(shop)
                .Fill(x => x.Postbox).Using(x => x.Address2)
                .Fill(x => x.StreetName).Using(x => x.Address)
                .Fill(x => x.PostalZone).Using(x => x.Zip)
                .Fill(x => x.CityName).Using(x => x.City)
                .FillEntity(x => x.Country).Using(GetSwedishCountry())
                .GetEntity();
        }

        protected SFTIContactType GetContact(IShop shop)
        {
            return new EntityFiller<IShop, SFTIContactType>(shop)
                .Fill(x => x.ElectronicMail).Using(x => x.Email)
                .Fill(x => x.Name).Using(x => x.Name)
                .Fill(x => x.Telefax).Using(x => x.Fax)
                .Fill(x => x.Telephone).Using(x => x.Phone)
                .GetEntity();
        }

        protected List<SFTIPartyIdentificationType> GetPartyIdentification(IShop shop)
        {
            var value = Formatter.FormatOrganizationNumber(shop.OrganizationNumber);
            return new List<SFTIPartyIdentificationType>
            {
                new SFTIPartyIdentificationType { ID = new IdentifierType { Value = value } }
            };
        }

        protected List<SFTIPartyTaxSchemeType> GetTaxScheme(SvefakturaConversionSettings settings)
        {
            return new List<SFTIPartyTaxSchemeType>
            {
                new SFTIPartyTaxSchemeType
                {
                    CompanyID = new IdentifierType { Value = Formatter.FormatTaxAccountingCode(settings.TaxAccountingCode) },
                    RegistrationName = new RegistrationNameType { Value = settings.SellingOrganizationName },
                    RegistrationAddress = settings.RegistrationAdress,
                    TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "VAT" } },
                },
                new SFTIPartyTaxSchemeType
                {
                    CompanyID = new IdentifierType { Value = Formatter.FormatOrganizationNumber(settings.SellingOrganizationNumber), },
                    ExemptionReason = new ReasonType { Value = settings.ExemptionReason },
                    RegistrationAddress = new SFTIAddressType { CityName = settings.RegistrationAdress.CityName, Country = GetSwedishCountry() },
                    TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "SWT" } }
                }
            };
        }
    }
}