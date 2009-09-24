using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Address", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class AddressType {
    
		private IdentifierType idField;
    
		private PostboxType postboxField;
    
		private FloorType floorField;
    
		private RoomType roomField;
    
		private StreetNameType streetNameField;
    
		private StreetNameType additionalStreetNameField;
    
		private BuildingNameType buildingNameField;
    
		private BuildingNumberType buildingNumberField;
    
		private MailType inhouseMailField;
    
		private DepartmentType departmentField;
    
		private CityNameType cityNameField;
    
		private ZoneType postalZoneField;
    
		private CountrySubentityType countrySubentityField;
    
		private CodeType countrySubentityCodeField;
    
		private RegionType regionField;
    
		private DistrictType districtField;
    
		private TimezoneOffsetType timezoneOffsetField;

		private List<LineType> addressLineField;
    
		private CountryType countryField;
    
		private LocationCoordinateType locationCoordinateField;
    
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
		public FloorType Floor {
			get {
				return floorField;
			}
			set {
				floorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public RoomType Room {
			get {
				return roomField;
			}
			set {
				roomField = value;
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
		public StreetNameType AdditionalStreetName {
			get {
				return additionalStreetNameField;
			}
			set {
				additionalStreetNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public BuildingNameType BuildingName {
			get {
				return buildingNameField;
			}
			set {
				buildingNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public BuildingNumberType BuildingNumber {
			get {
				return buildingNumberField;
			}
			set {
				buildingNumberField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MailType InhouseMail {
			get {
				return inhouseMailField;
			}
			set {
				inhouseMailField = value;
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CountrySubentityType CountrySubentity {
			get {
				return countrySubentityField;
			}
			set {
				countrySubentityField = value;
			}
		}
    
		/// <remarks/>
		public CodeType CountrySubentityCode {
			get {
				return countrySubentityCodeField;
			}
			set {
				countrySubentityCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public RegionType Region {
			get {
				return regionField;
			}
			set {
				regionField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DistrictType District {
			get {
				return districtField;
			}
			set {
				districtField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TimezoneOffsetType TimezoneOffset {
			get {
				return timezoneOffsetField;
			}
			set {
				timezoneOffsetField = value;
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
		public CountryType Country {
			get {
				return countryField;
			}
			set {
				countryField = value;
			}
		}
    
		/// <remarks/>
		public LocationCoordinateType LocationCoordinate {
			get {
				return locationCoordinateField;
			}
			set {
				locationCoordinateField = value;
			}
		}
	}
}