using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Contextualization", Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0", IsNullable=false)]
	public partial class ContextualizationType {
    
		private List<ContextType> contextField = new List<ContextType>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Context")]
		public List<ContextType> Context {
			get {
				return this.contextField;
			}
			set {
				this.contextField = value;
			}
		}
	}
}