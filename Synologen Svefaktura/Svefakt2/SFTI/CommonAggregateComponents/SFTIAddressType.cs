using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Address", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIAddressType {
    
		private IdentifierType idField;
    
		private PostboxType postboxField;
    
		private StreetNameType streetNameField;
    
		private DepartmentType departmentField;
    
		private CityNameType cityNameField;
    
		private ZoneType postalZoneField;
    
		private List<LineType> addressLineField = new List<LineType>();
    
		private SFTICountryType countryField;
    
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PostboxType Postbox {
			get {
				return postboxField;
			}
			set {
				postboxField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public StreetNameType StreetName {
			get {
				return streetNameField;
			}
			set {
				streetNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DepartmentType Department {
			get {
				return departmentField;
			}
			set {
				departmentField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CityNameType CityName {
			get {
				return cityNameField;
			}
			set {
				cityNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ZoneType PostalZone {
			get {
				return postalZoneField;
			}
			set {
				postalZoneField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItem("Line", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0", IsNullable=false)]
		public List<LineType> AddressLine {
			get {
				return addressLineField;
			}
			set {
				addressLineField = value;
			}
		}
    
		/// <remarks/>
		public SFTICountryType Country {
			get {
				return countryField;
			}
			set {
				countryField = value;
			}
		}
	}
}