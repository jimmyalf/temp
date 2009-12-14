using System.Collections.Generic;
using System.Xml.Serialization;
using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:documents:BasicInvoice:1:0")]
	[System.Xml.Serialization.XmlRoot("Invoice", Namespace="urn:sfti:documents:BasicInvoice:1:0", IsNullable=false)]
	public class SFTIInvoiceType {
    
		private SFTISimpleIdentifierType idField;
    
		private IssueDateType issueDateField;
    
		private CodeType invoiceTypeCodeField;
    
		private NoteType noteField;
    
		private TaxPointDateType taxPointDateField;
    
		private CurrencyCodeType invoiceCurrencyCodeField;
    
		private CurrencyCodeType taxCurrencyCodeField;
    
		private LineItemCountNumericType lineItemCountNumericField;

		private List<SFTIDocumentReferenceType> additionalDocumentReferenceField;
    
		private SFTIBuyerPartyType buyerPartyField;
    
		private SFTISellerPartyType sellerPartyField;
    
		private SFTIDeliveryType deliveryField;

		private List<SFTIPaymentMeansType> paymentMeansField;
    
		private SFTIPaymentTermsType paymentTermsField;

		private List<SFTIAllowanceChargeType> allowanceChargeField;
    
		private SFTIExchangeRateType exchangeRateField;

		private List<SFTITaxTotalType> taxTotalField;
    
		private SFTILegalTotalType legalTotalField;

		private List<SFTIInvoiceLineType> invoiceLineField;

		private List<SFTIDocumentReferenceType> requisitionistDocumentReferenceField;

		private List<SFTIDocumentReferenceType> initialInvoiceDocumentReferenceField;
    
		private SFTIDeliveryTermsType deliveryTermsField;
    
		private SFTIPeriodType invoicingPeriodField;
    
		/// <remarks/>
		[PropertyValidationRule("SFTIInvoiceType.ID is missing.", ValidationType.RequiredNotNull)]
		public SFTISimpleIdentifierType ID {
			get {
				return idField;
			}
			set {
				idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		[PropertyValidationRule("SFTIInvoiceType.IssueDate is missing.", ValidationType.RequiredNotNull)]
		public IssueDateType IssueDate {
			get {
				return issueDateField;
			}
			set {
				issueDateField = value;
			}
		}
    
		/// <remarks/>
		[PropertyValidationRule("SFTIInvoiceType.InvoiceTypeCode is missing.", ValidationType.RequiredNotNull)]
		public CodeType InvoiceTypeCode {
			get {
				return invoiceTypeCodeField;
			}
			set {
				invoiceTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NoteType Note {
			get {
				return noteField;
			}
			set {
				noteField = value;
			}
		}
    
		/// <remarks/>
		public TaxPointDateType TaxPointDate {
			get {
				return taxPointDateField;
			}
			set {
				taxPointDateField = value;
			}
		}
    
		/// <remarks/>
		public CurrencyCodeType InvoiceCurrencyCode {
			get {
				return invoiceCurrencyCodeField;
			}
			set {
				invoiceCurrencyCodeField = value;
			}
		}
    
		/// <remarks/>
		public CurrencyCodeType TaxCurrencyCode {
			get {
				return taxCurrencyCodeField;
			}
			set {
				taxCurrencyCodeField = value;
			}
		}
    
		/// <remarks/>
		[PropertyValidationRule("SFTIInvoiceType.LineItemCountNumeric is missing.", ValidationType.RequiredNotNull)]
		public LineItemCountNumericType LineItemCountNumeric {
			get {
				return lineItemCountNumericField;
			}
			set {
				lineItemCountNumericField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("AdditionalDocumentReference")]
		public List<SFTIDocumentReferenceType> AdditionalDocumentReference {
			get {
				return additionalDocumentReferenceField;
			}
			set {
				additionalDocumentReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		[PropertyValidationRule("SFTIInvoiceType.BuyerParty is missing.", ValidationType.RequiredNotNull)]
		public SFTIBuyerPartyType BuyerParty {
			get {
				return buyerPartyField;
			}
			set {
				buyerPartyField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		[PropertyValidationRule("SFTIInvoiceType.SellerParty is missing.", ValidationType.RequiredNotNull)]
		public SFTISellerPartyType SellerParty {
			get {
				return sellerPartyField;
			}
			set {
				sellerPartyField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIDeliveryType Delivery {
			get {
				return deliveryField;
			}
			set {
				deliveryField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("PaymentMeans", Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		[PropertyValidationRule("SFTIInvoiceType.PaymentMeans can only contain 0-3 elements.", ValidationType.CollectionHasMaximumCountRequirement, 0, 3)]
		public List<SFTIPaymentMeansType> PaymentMeans {
			get {
				return paymentMeansField;
			}
			set {
				paymentMeansField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIPaymentTermsType PaymentTerms {
			get {
				return paymentTermsField;
			}
			set {
				paymentTermsField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("AllowanceCharge")]
		public List<SFTIAllowanceChargeType> AllowanceCharge {
			get {
				return allowanceChargeField;
			}
			set {
				allowanceChargeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIExchangeRateType ExchangeRate {
			get {
				return exchangeRateField;
			}
			set {
				exchangeRateField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TaxTotal", Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public List<SFTITaxTotalType> TaxTotal {
			get {
				return taxTotalField;
			}
			set {
				taxTotalField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		[PropertyValidationRule("SFTIInvoiceType.LegalTotal is missing.", ValidationType.RequiredNotNull)]
		public SFTILegalTotalType LegalTotal {
			get {
				return legalTotalField;
			}
			set {
				legalTotalField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("InvoiceLine", Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		[PropertyValidationRule("SFTIInvoiceType.InvoiceLine is missing.", ValidationType.RequiredNotNull)]
		[PropertyValidationRule("SFTIInvoiceType.InvoiceLine must contain one or more elements.", ValidationType.CollectionHasMinimumCountRequirement, 1)]
		public List<SFTIInvoiceLineType> InvoiceLine {
			get {
				return invoiceLineField;
			}
			set {
				invoiceLineField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("RequisitionistDocumentReference")]
		//[PropertyValidationRule("SFTIInvoiceType.RequisitionistDocumentReference is missing.", ValidationType.RequiredNotNull)]
		//[PropertyValidationRule("SFTIInvoiceType.RequisitionistDocumentReference must contain one or two elements.", ValidationType.CollectionHasMinumumAndMaximumCountRequirement, 1, 2)]
		public List<SFTIDocumentReferenceType> RequisitionistDocumentReference {
			get {
				return requisitionistDocumentReferenceField;
			}
			set {
				requisitionistDocumentReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("InitialInvoiceDocumentReference")]
		public List<SFTIDocumentReferenceType> InitialInvoiceDocumentReference {
			get {
				return initialInvoiceDocumentReferenceField;
			}
			set {
				initialInvoiceDocumentReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIDeliveryTermsType DeliveryTerms {
			get {
				return deliveryTermsField;
			}
			set {
				deliveryTermsField = value;
			}
		}
    
		/// <remarks/>
		public SFTIPeriodType InvoicingPeriod {
			get {
				return invoicingPeriodField;
			}
			set {
				invoicingPeriodField = value;
			}
		}
	}
}