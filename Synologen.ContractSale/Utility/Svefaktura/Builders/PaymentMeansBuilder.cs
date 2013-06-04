using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders
{
    public class PaymentMeansBuilder : ISvefakturaBuilder
    {
        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
            throw new NotImplementedException();
            //TryAddPaymentMeans(invoice, settings.BankGiro, settings.BankgiroBankIdentificationCode, order.ContractCompany, settings);
            /*if (HasNotBeenSet(settings.InvoiceIssueDate) || HasNotBeenSet(giroNumber)) return;
                if (invoice.PaymentMeans == null) invoice.PaymentMeans = new List<SFTIPaymentMeansType>();
                invoice.PaymentMeans.Add(
                    new SFTIPaymentMeansType
                    {
                        PaymentMeansTypeCode = new PaymentMeansCodeType { Value = PaymentMeansCodeContentType.Item1 },
                        PayeeFinancialAccount = new SFTIFinancialAccountType 
                        {
                            ID = new IdentifierType {Value = FormatGiroNumber(giroNumber)},
                            FinancialInstitutionBranch = TryGetValue(giroBIC, new SFTIBranchType
                            {
                                FinancialInstitution = new SFTIFinancialInstitutionType
                                {
                                    ID = new IdentifierType {Value = giroBIC}
                                }
                            })
                        },
                        DuePaymentDate = GetPaymentMeansDuePaymentDate(settings.InvoiceIssueDate, company)
                    }
                );*/
        }
    }
}