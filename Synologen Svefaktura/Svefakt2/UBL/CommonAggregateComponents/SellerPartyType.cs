using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("SellerParty", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SellerPartyType {
    
		private IdentifierType buyerAssignedAccountIDField;
    
		private IdentifierType sellerAssignedAccountIDField;
    
		private List<IdentifierType> additionalAccountIDField = new List<IdentifierType>();
    
		private PartyType partyField;
    
		private ContactType shippingContactField;
    
		private ContactType accountsContactField;
    
		private ContactType orderContactField;
    
		/// <remarks/>
		public IdentifierType BuyerAssignedAccountID {
			get {
				return this.buyerAssignedAccountIDField;
			}
			set {
				this.buyerAssignedAccountIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType SellerAssignedAccountID {
			get {
				return this.sellerAssignedAccountIDField;
			}
			set {
				this.sellerAssignedAccountIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AdditionalAccountID")]
		public List<IdentifierType> AdditionalAccountID {
			get {
				return this.additionalAccountIDField;
			}
			set {
				this.additionalAccountIDField = value;
			}
		}
    
		/// <remarks/>
		public PartyType Party {
			get {
				return this.partyField;
			}
			set {
				this.partyField = value;
			}
		}
    
		/// <remarks/>
		public ContactType ShippingContact {
			get {
				return this.shippingContactField;
			}
			set {
				this.shippingContactField = value;
			}
		}
    
		/// <remarks/>
		public ContactType AccountsContact {
			get {
				return this.accountsContactField;
			}
			set {
				this.accountsContactField = value;
			}
		}
    
		/// <remarks/>
		public ContactType OrderContact {
			get {
				return this.orderContactField;
			}
			set {
				this.orderContactField = value;
			}
		}
	}
}