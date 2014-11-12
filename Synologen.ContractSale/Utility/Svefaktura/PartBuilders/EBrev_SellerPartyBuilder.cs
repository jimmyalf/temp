using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class EBrev_SellerPartyBuilder : SellerPartyBuilder
    {
        public EBrev_SellerPartyBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter) : base(settings, formatter) { }

        public override void Build(IOrder order, SFTIInvoiceType invoice)
        {
            invoice.SellerParty = new SFTISellerPartyType
            {
                Party = GetParty(order.SellingShop),
                AccountsContact = GetAccountContactFormatted(Settings.Contact)
            };
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
    }
}