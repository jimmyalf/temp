using System;
using System.Linq;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura
{
    public abstract class SvefakturaHydratorBase
    {
        protected SvefakturaHydratorBase(SvefakturaConversionSettings settings, SvefakturaFormatter formatter)
        {
            Settings = settings;
            Formatter = formatter;
        }

        public SvefakturaConversionSettings Settings { get; set; }
        public SvefakturaFormatter Formatter { get; set; }

        /*
        TryAddBuyerParty(invoice, order.ContractCompany, order);
        TryAddPaymentMeans(invoice, settings.BankGiro, settings.BankgiroBankIdentificationCode, order.ContractCompany, settings);
        TryAddPaymentMeans(invoice, settings.Postgiro, settings.PostgiroBankIdentificationCode, order.ContractCompany, settings);
        TryAddInvoiceLines(settings, invoice, order.OrderItems, settings.VATAmount);
        TryAddGeneralInvoiceInformation(invoice, settings, order, order.OrderItems);
        TryAddPaymentTerms(invoice, settings, order.ContractCompany);
     */

        public static bool AllAreNullOrEmpty(params object[] args)
        {
            return args.Where(value => value != null)
                    .Where(value => !(value is string) || !HasNotBeenSet(value as string))
                    .All(value => value is decimal? && HasNotBeenSet(value as decimal?));
        }

        public static bool HasNotBeenSet(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool HasNotBeenSet(decimal? value)
        {
            return !value.HasValue;
        }

        public static bool HasNotBeenSet(DateTime value)
        {
            return value.Equals(DateTime.MinValue);
        }

        public static bool OneOrMoreHaveValue(params object[] args)
        {
            return !AllAreNullOrEmpty(args);
        }

        public SFTICountryType GetSwedishCountry()
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
    }
}