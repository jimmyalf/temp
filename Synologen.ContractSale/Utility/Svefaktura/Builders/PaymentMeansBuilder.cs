using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders
{
    public class PaymentMeansBuilder : SvefakturaBuilder, ISvefakturaBuilder
    {
        public PaymentMeansBuilder(SvefakturaConversionSettings settings, SvefakturaFormatter formatter)
            : base(settings, formatter) { }

        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
            invoice.PaymentMeans = new List<SFTIPaymentMeansType>();
            if (!string.IsNullOrEmpty(Settings.BankGiro) || !string.IsNullOrEmpty(Settings.BankgiroBankIdentificationCode))
            {
                invoice.PaymentMeans.Add(new SFTIPaymentMeansType
                {
                    PaymentMeansTypeCode = new PaymentMeansCodeType { Value = PaymentMeansCodeContentType.Item1 },
                    PayeeFinancialAccount = GetPayeeFinancialAccount(x => x.BankgiroBankIdentificationCode, x => x.BankGiro),
                    DuePaymentDate = GetPaymentMeansDuePaymentDate(Settings, order.ContractCompany)
                });
            }

            if (!string.IsNullOrEmpty(Settings.Postgiro) || !string.IsNullOrEmpty(Settings.PostgiroBankIdentificationCode))
            {
                invoice.PaymentMeans.Add(new SFTIPaymentMeansType
                {
                    PaymentMeansTypeCode = new PaymentMeansCodeType { Value = PaymentMeansCodeContentType.Item1 },
                    PayeeFinancialAccount = GetPayeeFinancialAccount(x => x.PostgiroBankIdentificationCode, x => x.Postgiro),
                    DuePaymentDate = GetPaymentMeansDuePaymentDate(Settings, order.ContractCompany)
                });                
            }
        }

        protected virtual SFTIFinancialAccountType GetPayeeFinancialAccount(Func<SvefakturaConversionSettings, string> giroIdProperty, Func<SvefakturaConversionSettings, string> giroNumberProperty)
        {
            var giro = giroNumberProperty(Settings);
            var giroId = giroIdProperty(Settings);

            return new SFTIFinancialAccountType
            {
                ID = new IdentifierType { Value = Formatter.FormatGiroNumber(giro) },
                FinancialInstitutionBranch = new SFTIBranchType
                {
                    FinancialInstitution = new SFTIFinancialInstitutionType
                    {
                        ID = new IdentifierType { Value = giroId }
                    }
                }
            };
        }

        protected virtual PaymentDateType GetPaymentMeansDuePaymentDate(SvefakturaConversionSettings settings, ICompany company)
        {
            if (company == null)
            {
                return null;
            }

            return company.PaymentDuePeriod <= 0 
                ? null 
                : new PaymentDateType { Value = settings.InvoiceIssueDate.AddDays(company.PaymentDuePeriod) };
        }
    }
}