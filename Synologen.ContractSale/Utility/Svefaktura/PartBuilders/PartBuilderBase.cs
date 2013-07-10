using System;
using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders.Helpers;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.SpecializedDatatypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using NameType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public abstract class PartBuilderBase
    {
        protected PartBuilderBase(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter)
        {
            Settings = settings;
            Formatter = formatter;
        }

        public ISvefakturaConversionSettings Settings { get; set; }
        public ISvefakturaFormatter Formatter { get; set; }

        public BuildSourceContext<TDestination> Build<TDestination>() where TDestination : new()
        {
            return new BuildSourceContext<TDestination>();
        }

        protected List<NameType> GetPartyName<TEntity>(TEntity entity, Func<TEntity, string> nameProperty) where TEntity : class
        {
            return new List<NameType>
            {
                new NameType { Value = entity.Return(nameProperty, null) }
            };
        }

        protected List<SFTIPartyIdentificationType> GetPartyIdentification<TEntity>(TEntity entity, Func<TEntity, string> identificationProperty) where TEntity : class
        {
            var id = identificationProperty(entity);
            return new List<SFTIPartyIdentificationType>
            {
                new SFTIPartyIdentificationType
                {
                    ID = new IdentifierType { Value = Formatter.FormatOrganizationNumber(id) }
                }
            };
        }

        protected TEntity GetTextEntity<TEntity>(string value) where TEntity : Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes.TextType, new()
        {
            return value == null ? null : new TEntity { Value = value };
        }

        protected virtual SFTICountryType GetSwedishCountry()
        {
            return new SFTICountryType
            {
                IdentificationCode = new CountryIdentificationCodeType 
                {
                    Value = CountryIdentificationCodeContentType.SE,
                    name = "Sverige"
                }
            };
        }

        protected virtual SFTICountryType GetCountry(ICountry country)
        {
            if (country != null && country.OrganizationCountryCodeId.HasValue)
            {
                return new SFTICountryType
                {
                    IdentificationCode = new CountryIdentificationCodeType
                    {
                        name = country.Name, 
                        Value = (CountryIdentificationCodeContentType)country.OrganizationCountryCodeId
                    }
                };
            }

            return null;
        }

        protected virtual T GetAmountInSEK<T>(double value) where T : UBLAmountType, new()
        {
            return GetAmountInSEK<T>((decimal)value);
        }

        protected virtual T GetAmountInSEK<T>(decimal value) where T : UBLAmountType, new()
        {
            return new T { Value = value, amountCurrencyID = "SEK" };
        }

        protected virtual IdentifierType GetIdentifier(string value)
        {
            return new IdentifierType { Value = Formatter.FormatTaxAccountingCode(value) };
        }

        protected virtual IdentifierType GetIdentifier(string value, Func<string, string> formatterFunction)
        {
            return GetIdentifier(formatterFunction(value));
        }

        protected virtual IdentifierType GetIdentifier(Func<ISvefakturaConversionSettings, string> settingsValue, Func<string, string> formatterFunction)
        {
            var value = settingsValue(Settings);
            return GetIdentifier(formatterFunction(value));
        }

        protected virtual SFTIContactType GetAccountContactFormatted(SFTIContactType contact)
        {
            return Build<SFTIContactType>().With(contact)
                .Fill(x => x.ElectronicMail).Using(x => x.With(a => a.ElectronicMail).Return(a => a.Value, null))
                .Fill(x => x.Name).Using(x => x.With(a => a.Name).Return(a => a.Value, null))
                .Fill(x => x.Telefax).Using(x => x.With(a => a.Telefax).Return(a => a.Value, null), Formatter.FormatPhoneNumber)
                .Fill(x => x.Telephone).Using(x => x.With(a => a.Telephone).Return(a => a.Value, null), Formatter.FormatPhoneNumber)
                .GetEntity();
        }
    }
}