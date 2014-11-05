using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.EDI.Common;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;

namespace Spinit.Wpc.Synologen.EDI {
	public class Invoice : InvoiceComponent {
		private readonly float vatAmount;
		private readonly int setNumberOfDecimalsInRoundedValues = 2;
		public readonly string messageReference = EDIUtility.GetNewRandomAlphaNumericString(5, true);
		private readonly float _totalPriceIncludingVAT;
		private readonly float _totalPriceExcludingVAT;

		/// <summary>
		/// Creates an Invoice instance
		/// </summary>
		/// <param name="amountOfVAT">Amount of VAT used</param>
		public Invoice(float amountOfVAT) {
			vatAmount = amountOfVAT;
		}

		public Invoice(float amountOfVAT, int numberOfDecimalsInRoundedValues) {
			vatAmount = amountOfVAT;
			setNumberOfDecimalsInRoundedValues = numberOfDecimalsInRoundedValues;
		}

		public Invoice(float amountOfVAT, int numberOfDecimalsInRoundedValues, float totalPriceIncludingVAT, float totalPriceExcludingVAT) {
			vatAmount = amountOfVAT;
			setNumberOfDecimalsInRoundedValues = numberOfDecimalsInRoundedValues;
			_totalPriceIncludingVAT = totalPriceIncludingVAT;
			_totalPriceExcludingVAT = totalPriceExcludingVAT;
		}

		public float VatAmount {
			get { return vatAmount; }
		}

		public List<InvoiceRow> Articles { get; set; }

		//Invoice Header
		public InterchangeHeader InterchangeHeader { get; set; }
		public string MessageReference {get { return messageReference; }}
		public string DocumentNumber { get; set; }
		public DateTime InvoiceCreatedDate { get; set; }
		public string VendorOrderNumber { get; set; }
		public string BuyerRSTNumber { get; set; }
		public string BuyerOrderNumber { get; set; }
		public Buyer Buyer { get; set; }
		public Supplier Supplier { get; set; }
		public InvoiceSetting InvoiceSetting { get; set; }

		//Invoice Footer	
		//public int NumberOfRowItemsInMessage { get; set; }
		public float TotalPriceExcludingVAT {
			get {
				if (_totalPriceExcludingVAT>0) return _totalPriceExcludingVAT;
				return EDIUtility.TryRound(GetTotalPriceExcludingVAT(),setNumberOfDecimalsInRoundedValues);
			}
		}
		public float TotalPriceIncludingVAT {
			get {
				if (_totalPriceIncludingVAT > 0) return _totalPriceIncludingVAT;
				return EDIUtility.TryRound(EDIUtility.ConvertToPriceIncludingVAT(Articles, vatAmount),setNumberOfDecimalsInRoundedValues);
			}
		}
		public float TotalVATAmount {
			get {return EDIUtility.TryRound(TotalPriceIncludingVAT - TotalPriceExcludingVAT,setNumberOfDecimalsInRoundedValues);}

		}

		public string InterchangeControlReference {
			get { return InterchangeHeader.ControlReference; }
		}


		public override IParsedComponent Parse(){return new ParsedInvoice(this);}



		private float GetTotalPriceExcludingVAT() {
			return EDIUtility.GetTotalArticleSumExcludingVAT(Articles);
		}



		//private int GetTotalNumberOfMessageSegments() {
		//    var count = 0;
		//    count += base.GetNumberOfSegmentsInComponent();
		//    foreach(var article in Articles){
		//        count += article.GetNumberOfSegmentsInComponent();
				
		//    }
		//    return count;
		//}


	}
}