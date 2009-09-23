using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("TransportHandlingUnit", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class TransportHandlingUnitType {
    
		private IdentifierType idField;
    
		private CodeType unitTypeCodeField;
    
		private List<DespatchLineType> despatchLineField;
    
		private List<PackageType> actualPackageField = new List<PackageType>();
    
		private List<ReceiptLineType> receivedReceiptLineField = new List<ReceiptLineType>();
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		public CodeType UnitTypeCode {
			get {
				return this.unitTypeCodeField;
			}
			set {
				this.unitTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DespatchLine")]
		public List<DespatchLineType> DespatchLine {
			get {
				return this.despatchLineField;
			}
			set {
				this.despatchLineField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ActualPackage")]
		public List<PackageType> ActualPackage {
			get {
				return this.actualPackageField;
			}
			set {
				this.actualPackageField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReceivedReceiptLine")]
		public List<ReceiptLineType> ReceivedReceiptLine {
			get {
				return this.receivedReceiptLineField;
			}
			set {
				this.receivedReceiptLineField = value;
			}
		}
	}
}