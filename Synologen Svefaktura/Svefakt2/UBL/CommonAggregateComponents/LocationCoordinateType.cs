using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("LocationCoordinate", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class LocationCoordinateType {
    
		private CodeType coordinateSystemCodeField;
    
		private LatitudeDegreesMeasureType latitudeDegreesMeasureField;
    
		private LatitudeMinutesMeasureType latitudeMinutesMeasureField;
    
		private LatitudeDirectionCodeType latitudeDirectionCodeField;
    
		private LongitudeDegreesMeasureType longitudeDegreesMeasureField;
    
		private LongitudeMinutesMeasureType longitudeMinutesMeasureField;
    
		private LongitudeDirectionCodeType longitudeDirectionCodeField;
    
		/// <remarks/>
		public CodeType CoordinateSystemCode {
			get {
				return this.coordinateSystemCodeField;
			}
			set {
				this.coordinateSystemCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LatitudeDegreesMeasureType LatitudeDegreesMeasure {
			get {
				return this.latitudeDegreesMeasureField;
			}
			set {
				this.latitudeDegreesMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LatitudeMinutesMeasureType LatitudeMinutesMeasure {
			get {
				return this.latitudeMinutesMeasureField;
			}
			set {
				this.latitudeMinutesMeasureField = value;
			}
		}
    
		/// <remarks/>
		public LatitudeDirectionCodeType LatitudeDirectionCode {
			get {
				return this.latitudeDirectionCodeField;
			}
			set {
				this.latitudeDirectionCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LongitudeDegreesMeasureType LongitudeDegreesMeasure {
			get {
				return this.longitudeDegreesMeasureField;
			}
			set {
				this.longitudeDegreesMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LongitudeMinutesMeasureType LongitudeMinutesMeasure {
			get {
				return this.longitudeMinutesMeasureField;
			}
			set {
				this.longitudeMinutesMeasureField = value;
			}
		}
    
		/// <remarks/>
		public LongitudeDirectionCodeType LongitudeDirectionCode {
			get {
				return this.longitudeDirectionCodeField;
			}
			set {
				this.longitudeDirectionCodeField = value;
			}
		}
	}
}