using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("OrderLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class OrderLineType {
    
		private SubstitutionStatusCodeType substitutionStatusCodeField;
    
		private NoteType noteField;
    
		private LineItemType lineItemField;
    
		private List<LineItemType> sellerProposedLineItemField = new List<LineItemType>();
    
		private List<LineItemType> sellerSubstitutedLineItemField = new List<LineItemType>();

		private List<LineItemType> buyerProposedLineItemField = new List<LineItemType>();
    
		/// <remarks/>
		public SubstitutionStatusCodeType SubstitutionStatusCode {
			get {
				return substitutionStatusCodeField;
			}
			set {
				substitutionStatusCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NoteType Note {
			get {
				return noteField;
			}
			set {
				noteField = value;
			}
		}
    
		/// <remarks/>
		public LineItemType LineItem {
			get {
				return lineItemField;
			}
			set {
				lineItemField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("SellerProposedLineItem")]
		public List<LineItemType> SellerProposedLineItem {
			get {
				return sellerProposedLineItemField;
			}
			set {
				sellerProposedLineItemField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("SellerSubstitutedLineItem")]
		public List<LineItemType> SellerSubstitutedLineItem {
			get {
				return sellerSubstitutedLineItemField;
			}
			set {
				sellerSubstitutedLineItemField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("BuyerProposedLineItem")]
		public List<LineItemType> BuyerProposedLineItem {
			get {
				return buyerProposedLineItemField;
			}
			set {
				buyerProposedLineItemField = value;
			}
		}
	}
}