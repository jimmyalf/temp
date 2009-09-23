using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("AddressLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class AddressLineType {
    
		private List<LineType> lineField = new List<LineType>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Line", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public List<LineType> Line {
			get {
				return this.lineField;
			}
			set {
				this.lineField = value;
			}
		}
	}
}