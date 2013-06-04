using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders
{
    public class BuyerPartyBuilder : SvefakturaBuilderBase, ISvefakturaBuilder
    {
        public BuyerPartyBuilder(SvefakturaConversionSettings settings, SvefakturaFormatter formatter)
            : base(settings, formatter) { }

        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
            if (invoice.BuyerParty == null)
            {
                return;
            }

            var company = order.ContractCompany;
            invoice.BuyerParty = new SFTIBuyerPartyType
            {
                Party = new SFTIPartyType
                {
                    PartyName = GetPartyName(company, x => x.InvoiceCompanyName),
                    Address = GetAddress(order),
                    Contact = GetContact(order),
                    PartyTaxScheme = GetTaxScheme(company),
                    PartyIdentification = GetPartyIdentification(company, x => x.OrganizationNumber)
                }
            };
        }

        protected List<SFTIPartyTaxSchemeType> GetTaxScheme(ICompany company)
        {
            return new List<SFTIPartyTaxSchemeType>
            {
                new SFTIPartyTaxSchemeType
                {
                    CompanyID = new IdentifierType { Value = Formatter.FormatTaxAccountingCode(company.TaxAccountingCode) },
                    RegistrationName = new RegistrationNameType { Value = company.OrganizationNumber },
                    RegistrationAddress = Build<SFTIAddressType>().With(company)
                        .Fill(x => x.CityName).Using(x => x.City)
                        .Fill(x => x.Postbox).Using(x => x.PostBox)
                        .Fill(x => x.StreetName).Using(x => x.StreetName)
                        .Fill(x => x.PostalZone).Using(x => x.Zip)
                        .FillEntity(x => x.Country).Using(GetCountry(company.Country))
                        .GetEntity(),
                    TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "VAT" } },
                },
                new SFTIPartyTaxSchemeType
                {
                    CompanyID = new IdentifierType { Value = Formatter.FormatOrganizationNumber(company.OrganizationNumber), },
                    RegistrationAddress = Build<SFTIAddressType>().With(company)
                        .Fill(x => x.CityName).Using(x => x.City)
                        .FillEntity(x => x.Country).Using(GetCountry(company.Country))
                        .GetEntity(),
                    TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "SWT" } }
                }
            };
        }

        protected SFTIContactType GetContact(IOrder order)
        {
            return Build<SFTIContactType>().With(order)
                .Fill(x => x.ElectronicMail).Using(x => x.Email)
                .Fill(x => x.Name).Using(x => x.CustomerCombinedName)
                .Fill(x => x.Telephone).Using(x => x.Phone)
                .GetEntity();
        }

        protected SFTIAddressType GetAddress(IOrder order)
        {
            var company = order.ContractCompany;
            return Build<SFTIAddressType>().With(company)
                .Fill(x => x.Postbox).Using(x => x.PostBox)
                .Fill(x => x.StreetName).Using(x => x.StreetName)
                .Fill(x => x.PostalZone).Using(x => x.Zip)
                .Fill(x => x.CityName).Using(x => x.City)
                .FillEntity(x => x.Department).Using(new DepartmentType { Value = order.CompanyUnit })
                .FillEntity(x => x.Country).Using(GetCountry(company.Country))
                .GetEntity();
        }
    }
}