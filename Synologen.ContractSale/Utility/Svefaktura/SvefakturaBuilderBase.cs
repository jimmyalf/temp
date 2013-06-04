using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Helpers;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using NameType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura
{
    public abstract class SvefakturaBuilderBase
    {
        protected SvefakturaBuilderBase(SvefakturaConversionSettings settings, SvefakturaFormatter formatter)
        {
            Settings = settings;
            Formatter = formatter;
        }

        public SvefakturaConversionSettings Settings { get; set; }
        public SvefakturaFormatter Formatter { get; set; }

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
            return new TEntity { Value = value };
        }

        protected virtual bool AllAreNullOrEmpty(params object[] args)
        {
            return args.Where(value => value != null)
                    .Where(value => !(value is string) || !HasNotBeenSet(value as string))
                    .All(value => value is decimal? && HasNotBeenSet(value as decimal?));
        }

        protected virtual bool HasNotBeenSet(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        protected virtual bool HasNotBeenSet(decimal? value)
        {
            return !value.HasValue;
        }

        protected virtual bool HasNotBeenSet(DateTime value)
        {
            return value.Equals(DateTime.MinValue);
        }

        protected virtual bool OneOrMoreHaveValue(params object[] args)
        {
            return !AllAreNullOrEmpty(args);
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
    }
}