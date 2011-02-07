using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("LocationCoordinate", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTILocationCoordinateType {
    
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
				return coordinateSystemCodeField;
			}
			set {
				coordinateSystemCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LatitudeDegreesMeasureType LatitudeDegreesMeasure {
			get {
				return latitudeDegreesMeasureField;
			}
			set {
				latitudeDegreesMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LatitudeMinutesMeasureType LatitudeMinutesMeasure {
			get {
				return latitudeMinutesMeasureField;
			}
			set {
				latitudeMinutesMeasureField = value;
			}
		}
    
		/// <remarks/>
		public LatitudeDirectionCodeType LatitudeDirectionCode {
			get {
				return latitudeDirectionCodeField;
			}
			set {
				latitudeDirectionCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LongitudeDegreesMeasureType LongitudeDegreesMeasure {
			get {
				return longitudeDegreesMeasureField;
			}
			set {
				longitudeDegreesMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LongitudeMinutesMeasureType LongitudeMinutesMeasure {
			get {
				return longitudeMinutesMeasureField;
			}
			set {
				longitudeMinutesMeasureField = value;
			}
		}
    
		/// <remarks/>
		public LongitudeDirectionCodeType LongitudeDirectionCode {
			get {
				return longitudeDirectionCodeField;
			}
			set {
				longitudeDirectionCodeField = value;
			}
		}
	}
}