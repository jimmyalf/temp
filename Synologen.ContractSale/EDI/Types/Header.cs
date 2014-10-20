using System;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;
using Spinit.Wpc.Synologen.EDI.Common.Types;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class Header : InvoiceComponent {
		//public const int NumberOfSegmentsNotToBeIncludedInSegmentCount = 2;



		public new ParsedHeader Parse() { return new ParsedHeader(this); }

		public new int GetNumberOfSegmentsInComponent() {
			//UNA & UNB segments should not be included in count
			return base.GetNumberOfSegmentsInComponent() - NumberOfSegmentsNotToBeIncludedInSegmentCount;
			//var count = 0;
			//if(InterchangeHeader != null) count++;
			//if(String.IsNullOrEmpty(MessageIdentifier)) count++;
			//if(InvoiceCreatedDate != DateTime.MinValue) count++;
			//if(String.IsNullOrEmpty(VendorOrderNumber)) count++;
			//if(String.IsNullOrEmpty(BuyerRSTNumber)) count++;
			//if(String.IsNullOrEmpty(BuyerOrderNumber)) count++;
			//count += Buyer.GetNumberOfSegmentsInComponent();
			//count += Supplier.GetNumberOfSegmentsInComponent();
			//count += InvoiceSetting.GetNumberOfSegmentsInComponent();
			//return count;
		}

	}
}