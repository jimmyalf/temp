using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Invoicing.Types
{
	public class EDIConversionSettings 
    {
		public string InvoiceCurrencyCode;
		public string Postgiro { get; set;}
		public string BankGiro { get; set;}
		public float VATAmount { get; set;}
		public EdiAddress SenderEdiAddress { get; set; }
		public int NumberOfDecimalsUsedAtRounding { get; set; }
	}
}