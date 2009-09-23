using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("PartyName", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIPartyNameType {
    
		private List<NameType1> nameField = new List<NameType1>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("Name", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public List<NameType1> Name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}
	}
}