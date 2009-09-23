namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("SellerParty", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
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