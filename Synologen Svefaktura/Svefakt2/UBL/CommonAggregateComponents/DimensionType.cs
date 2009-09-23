using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Dimension", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class DimensionType {
    
		private IdentifierType attributeIDField;
    
		private MeasureType2 measureField;
    
		private DescriptionType descriptionField;
    
		private MeasureType2 minimumMeasureField;
    
		private MeasureType2 maximumMeasureField;
    
		/// <remarks/>
		public IdentifierType AttributeID {
			get {
				return this.attributeIDField;
			}
			set {
				this.attributeIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MeasureType2 Measure {
			get {
				return this.measureField;
			}
			set {
				this.measureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DescriptionType Description {
			get {
				return this.descriptionField;
			}
			set {
				this.descriptionField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MeasureType2 MinimumMeasure {
			get {
				return this.minimumMeasureField;
			}
			set {
				this.minimumMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MeasureType2 MaximumMeasure {
			get {
				return this.maximumMeasureField;
			}
			set {
				this.maximumMeasureField = value;
			}
		}
	}
}