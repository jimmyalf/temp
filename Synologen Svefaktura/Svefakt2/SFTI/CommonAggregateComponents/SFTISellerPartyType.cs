namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("SellerParty", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTISellerPartyType {
    
		private SFTIPartyType partyField;
    
		private SFTIContactType accountsContactField;
    
		/// <remarks/>
		public SFTIPartyType Party {
			get {
				return this.partyField;
			}
			set {
				this.partyField = value;
			}
		}
    
		/// <remarks/>
		public SFTIContactType AccountsContact {
			get {
				return this.accountsContactField;
			}
			set {
				this.accountsContactField = value;
			}
		}
	}
}