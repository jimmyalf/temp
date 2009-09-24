using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("AdditionalItemIdentification", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class ItemIdentificationType {
    
		private IdentifierType idField;

		private List<PhysicalAttributeType> physicalAttributeField;

		private List<DimensionType> measurementDimensionField;
    
		private PartyType issuerPartyField;
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return idField;
			}
			set {
				idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("PhysicalAttribute")]
		public List<PhysicalAttributeType> PhysicalAttribute {
			get {
				return physicalAttributeField;
			}
			set {
				physicalAttributeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("MeasurementDimension")]
		public List<DimensionType> MeasurementDimension {
			get {
				return measurementDimensionField;
			}
			set {
				measurementDimensionField = value;
			}
		}
    
		/// <remarks/>
		public PartyType IssuerParty {
			get {
				return issuerPartyField;
			}
			set {
				issuerPartyField = value;
			}
		}
	}
}