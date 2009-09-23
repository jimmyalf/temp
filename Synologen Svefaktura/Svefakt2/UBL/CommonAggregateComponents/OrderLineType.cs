using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("OrderLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class OrderLineType {
    
		private SubstitutionStatusCodeType substitutionStatusCodeField;
    
		private NoteType noteField;
    
		private LineItemType lineItemField;
    
		private List<LineItemType> sellerProposedLineItemField = new List<LineItemType>();
    
		private List<LineItemType> sellerSubstitutedLineItemField = new List<LineItemType>();

		private List<LineItemType> buyerProposedLineItemField = new List<LineItemType>();
    
		/// <remarks/>
		public SubstitutionStatusCodeType SubstitutionStatusCode {
			get {
				return this.substitutionStatusCodeField;
			}
			set {
				this.substitutionStatusCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NoteType Note {
			get {
				return this.noteField;
			}
			set {
				this.noteField = value;
			}
		}
    
		/// <remarks/>
		public LineItemType LineItem {
			get {
				return this.lineItemField;
			}
			set {
				this.lineItemField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("SellerProposedLineItem")]
		public List<LineItemType> SellerProposedLineItem {
			get {
				return this.sellerProposedLineItemField;
			}
			set {
				this.sellerProposedLineItemField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("SellerSubstitutedLineItem")]
		public List<LineItemType> SellerSubstitutedLineItem {
			get {
				return this.sellerSubstitutedLineItemField;
			}
			set {
				this.sellerSubstitutedLineItemField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("BuyerProposedLineItem")]
		public List<LineItemType> BuyerProposedLineItem {
			get {
				return this.buyerProposedLineItemField;
			}
			set {
				this.buyerProposedLineItemField = value;
			}
		}
	}
}