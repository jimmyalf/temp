using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:BuyerParty", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class BuyerPartyType {
    
		private IdentifierType buyerAssignedAccountIDField;
    
		private IdentifierType sellerAssignedAccountIDField;
    
		private List<IdentifierType> additionalAccountIDField = new List<IdentifierType>();
    
		private PartyType partyField;
    
		/// <remarks/>
		public IdentifierType BuyerAssignedAccountID {
			get {
				return buyerAssignedAccountIDField;
			}
			set {
				buyerAssignedAccountIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType SellerAssignedAccountID {
			get {
				return sellerAssignedAccountIDField;
			}
			set {
				sellerAssignedAccountIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("AdditionalAccountID")]
		public List<IdentifierType> AdditionalAccountID {
			get {
				return additionalAccountIDField;
			}
			set {
				additionalAccountIDField = value;
			}
		}
    
		/// <remarks/>
		public PartyType Party {
			get {
				return partyField;
			}
			set {
				partyField = value;
			}
		}
	}
}